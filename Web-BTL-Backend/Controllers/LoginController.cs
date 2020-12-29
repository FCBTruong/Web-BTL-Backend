using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Web_BTL_Backend.Models;
using Web_BTL_Backend.Models.ClientSendForm;
using Web_BTL_Backend.Models.Data;

namespace Web_BTL_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public db_a6a86f_truongContext _context;

        public LoginController(IConfiguration config, db_a6a86f_truongContext dbContext)
        {
            this._config = config;
            this._context = dbContext;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LogInForm loginForm)
        {
            string username = loginForm.username;
            string password = loginForm.password;
            if (username == null || password == null) return BadRequest("Not Null " + username + password);

            if (!RegexChecker.checkAuthString(username)) return BadRequest("Valid username");
            if (!RegexChecker.checkAuthString(password)) return BadRequest("Valid password");

            UserModel login = new UserModel();
            login.UserName = username;
            login.Password = password;
            IActionResult response = Unauthorized();


            var user = AuthenticateUser(login);

            if (user != null)
            {
                var tokenStr = GenerateJSONWebToken(user);
                response = Ok(new { token = tokenStr });
            }
            return response;
        }

        private UserModel AuthenticateUser(UserModel login)
        {
            UserModel user = null;
            var accs = _context.Auths.Where(user => user.UserName == login.UserName).ToList();
            if (accs == null || accs.Count == 0) return user;
            Auths acc = accs[0];

            if (!HashPassword.VerifyHashedPassword(acc.Password, login.Password)) return user;

            var role = (from u in _context.Users
                        join r
in _context.Roles on u.IdRole equals r.IdRole
                        where u.IdUser == acc.IdUser
                        select r.RoleName).ToList();
            user = new UserModel
            {
                IdUser = acc.IdUser,
                UserName = acc.UserName,
                EmailAddress = acc.Email,
                Password = acc.Password,
                Role = role[0].ToString(),
            };

            return user;
        }

        private string GenerateJSONWebToken(UserModel userinfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userinfo.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, userinfo.Role),
                new Claim(JwtRegisteredClaimNames.NameId, userinfo.IdUser.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userinfo.EmailAddress),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
                );
            var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodetoken;
        }

        [Authorize]
        [HttpPost]
        [Route("TestAuth")]
        public string Post()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userName = claim[0].Value;
            var role = claim[1].Value;
            return "Welcom to: " + userName + " " + role;
        }

        [HttpGet("GetValue")]
        [Route("api/mmmpost")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "Value1", "Value2", "Value3" };
        }

        [Authorize]
        [HttpPost]
        [Route("RefreshToken")]
        public ActionResult<IEnumerable<string>> RefreshToken(string oldToken)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                var userName = claim[0].Value;
                var role = claim[1].Value;
                UserModel user = new UserModel
                {
                    IdUser = Int32.Parse(claim[2].Value),
                    UserName = claim[0].Value,
                    EmailAddress = claim[3].Value,
                    Password = "",
                    Role = claim[1].Value,
                };

                var newToken = GenerateJSONWebToken(user);
                return Ok(new { token = newToken }); ;
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e);
            }
        }
    }
}
