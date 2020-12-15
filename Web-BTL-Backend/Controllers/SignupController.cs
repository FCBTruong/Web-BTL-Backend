using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Web_BTL_Backend.Models;
using Web_BTL_Backend.Models.ClientSendForm;
using Web_BTL_Backend.Models.Data;

namespace Web_BTL_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        private IConfiguration _config;
        public db_a6a86f_truongContext _context;
        public SignupController(IConfiguration config, db_a6a86f_truongContext dbContext)
        {
            this._context = dbContext;
            this._config = config;
        }

        [HttpPost]
        public IActionResult Signup([FromBody] SignUpForm signUpForm)
        {
            string username = signUpForm.username;
            string password = signUpForm.password;

            IActionResult response = BadRequest("Same username");
            if (signUpForm == null) return response;
            if (!checkUsernameExsit(signUpForm.username)) return response;
            if (!checkFormat(signUpForm.username, signUpForm.password)) return UnprocessableEntity("Not Valid Password or UserName: request 8 <= size <= 30");
            if (createAccount(signUpForm)) return Ok("Create account successful");
            return response;
        }

        private bool checkUsernameExsit(string userName)
        {
            List<Auths> authList = _context.Auths.ToList();
            foreach (Auths auth in authList)
            {
                if (auth.UserName == userName) return false;
            }
            return true;
        }

        private bool checkFormat(string username, string password)
        {
            if (username.Length < 8 || username.Length > 30) return false;
            if (password.Length < 8 || password.Length > 30) return false;
            return true;
        }
        private bool createAccount(SignUpForm signUpForm)
        {
            UserModel user = new UserModel
            {
                UserName = signUpForm.username,
                Password = signUpForm.password,
            };

            Auths newAuth = new Auths
            {
                UserName = signUpForm.username,
                Password = signUpForm.password,
                Email = signUpForm.email,
            };
            _context.Auths.Add(newAuth);
            _context.SaveChanges();

            var a = _context.Auths.Where(a => a.UserName == signUpForm.username).ToList();
            int newIdUser = a[0].IdUser;

            Users newUser = new Users
            {
                IdUser = newIdUser,
                CreatedAt = DateTime.Today,
                UpdatedAt = DateTime.Today,
                Name = signUpForm.nameDisplay,
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return true;
        }

        [HttpGet]
        [Route("Message/GetDataJson")]
        public ActionResult<IEnumerable<string>> GetMM()
        {
            var s = "ddddhjjjjjjjjjjjjjjjjjjjjjjjjjjjj";
            return Ok(s);
        }
    }
}
