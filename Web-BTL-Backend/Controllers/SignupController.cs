using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Web_BTL_Backend.Models;
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
        public IActionResult Signup(string username, string password)
        {
            IActionResult response = BadRequest("Same username");
            if (username == null || password == null) return response;
            if (!checkUsernameExsit(username)) return response;
            if (!checkFormat(username, password)) return UnprocessableEntity("Not Valid Password or UserName: request 8 <= size <= 30");
            if (createAccount(username, password)) return Ok("Create account successful");
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
        private bool createAccount(string username, string password)
        {
            UserModel user = new UserModel
            {
                UserName = username,
                Password = password,
            };

            Auths newAuth = new Auths
            {
                UserName = username,
                Password = password,
                Email = "user@gmail.com",
            };
            _context.Auths.Add(newAuth);
            _context.SaveChanges();

            var a = _context.Auths.Where(a => a.UserName == username).ToList();
            int newIdUser = a[0].IdUser;

            Users newUser = new Users
            {
                IdUser = newIdUser,
                CreatedAt = DateTime.Today.Date,
                UpdatedAt = DateTime.Today.Date,
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
