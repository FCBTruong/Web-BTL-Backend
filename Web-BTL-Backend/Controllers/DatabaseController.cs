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
        public static MySqlConnection conn;
        public db_a6a86f_truongContext _context;
        public DatabaseController(db_a6a86f_truongContext dbContext)
        {
            _context = dbContext;
        }

        public static void Init(String connectionStr)
        {
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = connectionStr;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
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

        [HttpGet]
        [Route("/Test")]
        public IActionResult TestImage([FromForm(Name = "files")] List<IFormFile> files)
        {
            try
            {
                var _f = new FileServices();
                _f.SaveFile(files, "aa");
                return Ok();
                //  return Ok(files);
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }
    }
}
