using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Web_BTL_Backend.Models.Data;

namespace Web_BTL_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserInforController : ControllerBase
    {
        private IConfiguration _config;
        public db_a6a86f_truongContext _context;
        public UserInforController(IConfiguration config, db_a6a86f_truongContext dbContext)
        {
            this._config = config;
            this._context = dbContext;
        }

        [HttpGet]
        public IActionResult UserInfor()
        {
            return Ok();
        }

        [Authorize]
        [Route("GetUserInfor")]
        public IActionResult GetUserInfor()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var userId = claim[2].Value;

            var userInfor = _context.Users.Find(Int32.Parse(userId));
            return Ok(userInfor);
        }
    }
}
