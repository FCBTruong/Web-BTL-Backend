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
    public class ReviewPostController : ControllerBase
    {
        private IConfiguration _config;
        public db_a6a86f_truongContext _context;

        public ReviewPostController(IConfiguration config, db_a6a86f_truongContext dbContext)
        {
            this._config = config;
            this._context = dbContext;
        }

        [HttpGet]
        public IActionResult ReviewPost()
        {
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("PostReview")]
        public IActionResult PostNewReview([FromBody] ReviewForm reviewForm)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                var idUser = Int32.Parse(claim[2].Value);

                // check review exist:
                bool p = _context.Comments.Any(v => v.IdUser == idUser
                && v.IdPost == reviewForm.idPost);
                if (!p)
                {
                    _context.Comments.Add(new Comments
                    {
                        IdPost = reviewForm.idPost,
                        IdUser = idUser,
                        Content = reviewForm.content,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Rating = reviewForm.rate
                    });
                    _context.SaveChanges();
                    return Ok();
                }

                return BadRequest("Already exist review");
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("AcceptReview")]
        public IActionResult AcceptNewReview(int idReview)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                if (claim[1].Value != "admin") return Unauthorized("Only admin can use this function!");

                var review = _context.Comments.Find(idReview);
                review.Status = 1; // accept
                _context.SaveChanges();
                return Ok("Accept succesful!");
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("RemoveReview")]
        public IActionResult RemoveReview(int idReview)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                if (claim[1].Value != "admin") return Unauthorized("Only admin can use this function!");

                var review = _context.Comments.Find(idReview);
                _context.Comments.Remove(review);
                _context.SaveChanges();
                return Ok("Remove review with id: " + idReview);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetReviewPending")]
        public IActionResult GetReviewPending()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                if (claim[1].Value != "admin") return Unauthorized("Only admin can use this function!");

                var reviewPendingList = _context.Comments.Where(r => r.Status == 0);
                return Ok(reviewPendingList);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e);
            }
        }
    }
}
