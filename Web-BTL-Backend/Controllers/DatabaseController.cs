﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Web_BTL_Backend.Models.Data;

namespace Web_BTL_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DatabaseController : ControllerBase
    {
        // public static MySql.Data.MySqlClient.MySqlConnection conn;
        public static db_a6a86f_truongContext _context;
        public DatabaseController(db_a6a86f_truongContext dbContext)
        {
            _context = dbContext;
        }

        public static void Init(String connectionStr)
        {
            /*  try
              {
                  conn = new MySql.Data.MySqlClient.MySqlConnection();
                  conn.ConnectionString = connectionStr;
                  conn.Open();
              }
              catch (MySql.Data.MySqlClient.MySqlException ex)
              {

              }*/
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
            var listMotelRooms = _context.Motelrooms.Take(number).ToList();
            return Ok(listMotelRooms);
        }
    }
}