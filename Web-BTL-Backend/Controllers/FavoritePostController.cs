using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    public class FavoritePostController : ControllerBase
    {

        private IConfiguration _config;
        public db_a6a86f_truongContext _context;

        public FavoritePostController(IConfiguration config, db_a6a86f_truongContext dbContext)
        {
            this._config = config;
            this._context = dbContext;
        }

        [HttpGet]
        public IActionResult FavoritePost()
        {
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("GetFavoritePosts")]
        public IActionResult GetFavoritePosts()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                var idUser = Int32.Parse(claim[2].Value);

                var favoritePosts = (from fp in _context.FavoritePosts
                                     where fp.IdUser == idUser
                                     join p in _context.Posts on fp.IdPost equals p.IdPost
                                     join mr in _context.Motelrooms on p.IdRoom equals mr.IdRoom

                                     select (new
                                     {
                                         fp.IdPost,
                                         mr.Title,
                                         mr.Price,
                                         mr.IdUtility,
                                         mr.IdCategory,
                                         mr.Slug,
                                         imagePath = mr.RoomImages.Select(p => new { imagePath = p.ImagePath }).ToList()[0]
                                     }));

                return Ok(favoritePosts);

            }
            catch (Exception e)
            {
                return BadRequest("Error " + e);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("FavorPost")]
        public IActionResult FavorPost(int idPost)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                var idUser = Int32.Parse(claim[2].Value);

                _context.FavoritePosts.Add(new FavoritePosts
                {
                    IdPost = idPost,
                    IdUser = idUser,
                });
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Error " + e);
            }
        }


        [Authorize]
        [HttpPost]
        [Route("UnFavorPost")]
        public IActionResult UnFavorPost(int idPost)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();
                var idUser = Int32.Parse(claim[2].Value);

                var f = _context.FavoritePosts.Find(idUser, idPost);
                if (f == null) return BadRequest("NULL post!");
                _context.Remove(f);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest("Error " + e);
            }
        }
    }
}
