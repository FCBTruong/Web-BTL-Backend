using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Web_BTL_Backend.Models.ClientSendForm;
using Web_BTL_Backend.Models.Data;

namespace Web_BTL_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private IConfiguration _config;
        public db_a6a86f_truongContext _context;

        public ReportController(IConfiguration config, db_a6a86f_truongContext dbContext)
        {
            this._config = config;
            this._context = dbContext;
        }

        [HttpGet]
        public IActionResult Report()
        {
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("SendReport")]
        public IActionResult SendReport([FromBody] ReportForm report)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var idUser = Int32.Parse(claim[2].Value);
            try
            {
                _context.Reports.Add(
                    new Reports
                    {
                        IdPost = report.idPost,
                        IdUser = idUser,
                        Content = report.content,
                        CreatedAt = DateTime.Now,
                        ReportType = report.reportType
                    });
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetReports")]
        public IActionResult GetReports()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            var role = claim[1].Value;
            try
            {
                if (role != "admin") return Unauthorized("Only Admin!");
                var reports = from rp in _context.Reports join
                              p in _context.Posts on rp.IdPost equals p.IdPost
                              join mr in _context.Motelrooms on p.IdRoom equals mr.IdRoom
                              select (new
                              {
                                  rp.IdPost,
                                  rp.Content,
                                  rp.ReportType,
                                  rp.CreatedAt,
                                  rp.IdUser,
                                  mr.Title,
                                  mr.Slug,
                                  mr.IdCategory
                              });
                return Ok(reports);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e);
            }
        }
    }
}
