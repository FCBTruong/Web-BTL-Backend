using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using Web_BTL_Backend.Models;
using Web_BTL_Backend.Models.Data;

namespace Web_BTL_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        public db_a6a86f_truongContext _context;
        public DatabaseController(db_a6a86f_truongContext dbContext)
        {
            _context = dbContext;
        }

        public static void Init(String connectionStr)
        {
         
        }

        [HttpGet]
        [Route("/Message/GetDataJson")]
        public ActionResult<IEnumerable<string>> GetMM()
        {
            var s = "ddddhjjjjjjjjjjjjjjjjjjjjjjjjjjjj";
            return Ok(s);
        }

        [HttpGet]
        [Route("/Message/GetData")]
        public ActionResult<IEnumerable<string>> GetVVVV()
        {
            return Ok("");
        }

        [HttpGet]
        [Route("/GetUsers")]
        public ActionResult<IEnumerable<string>> GetUsers()
        {
            var listUsers = _context.Users.ToList();
            return Ok(listUsers);
        }

        [HttpGet]
        [Route("/GetMotelRooms")]
        public ActionResult<IEnumerable<string>> GetModelRooms(int number)
        {
            if (number == 0) return BadRequest();
            var listMotelRooms = _context.Motelrooms.Take(number).ToList();
            return Ok(listMotelRooms);
        }
    }
}
