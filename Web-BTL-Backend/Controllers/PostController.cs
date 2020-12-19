using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using Web_BTL_Backend.Models.ClientDataReturn;
using Web_BTL_Backend.Models.ClientSendForm;
using Web_BTL_Backend.Models.Data;

namespace Web_BTL_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IConfiguration _config;
        public db_a6a86f_truongContext _context;
        public PostController(IConfiguration config, db_a6a86f_truongContext dbContext)
        {
            this._config = config;
            this._context = dbContext;
        }

        [HttpGet]
        public IActionResult Post()
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetPostInfor")]
        public IActionResult GetPostInfor(int idPost)
        {
            try
            {
                PostInformation postInformation;

                var posts = (_context.Posts.Where(p => p.IdPost == idPost).ToList());
                if (posts == null || posts.Count == 0) return NoContent();
                Posts post = posts[0];
                if (post.Status == 0) return NoContent();

                Users owner = (_context.Users.Where(u => u.IdUser == post.IdUser).ToList())[0];
                Motelrooms motelInfor = (_context.Motelrooms.Where(m => m.IdRoom == post.IdRoom).ToList())[0];

                var comments = _context.Comments.Where(c => c.IdPost == post.IdPost).ToList();

                var category = (_context.Categories.Where(c => c.IdCategory == motelInfor.IdCategory).ToList())[0];
                List<PostComment> postComments = new List<PostComment>();
                for (int i = 0; i < comments.Count; i++)
                {
                    var commenter = (_context.Users.Where(u => u.IdUser == comments[i].IdUser).ToList())[0];
                    var pc = new PostComment
                    {
                        comment = comments[i],
                        commenter = commenter,
                    };
                    postComments.Add(pc);
                }

                if (DatabaseController.conn.State == System.Data.ConnectionState.Closed) DatabaseController.conn.Open();
                List<string> imagesList = new List<string>();
                string sql = "Select image_path from room_images where id_room=" + post.IdRoom.ToString();
                MySqlCommand cmd = new MySqlCommand(sql, DatabaseController.conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    imagesList.Add(rdr.GetString("image_path"));
                };

                rdr.Close();

                postInformation = new PostInformation
                {
                    post = post,
                    owner = owner,
                    motelInfor = motelInfor,
                    comments = postComments,
                    images = imagesList,
                    category = category,
                };

                return Ok(postInformation);
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        [HttpGet]
        [Route("GetPostInforBySlug")]
        public IActionResult GetPostInforBySlug(string slug)
        {
            int idRoom = (_context.Motelrooms.Where(u => u.Slug == slug).ToList())[0].IdRoom;
            var idPost = (_context.Posts.Where(u => u.IdRoom == idRoom).ToList())[0].IdPost;
            return GetPostInfor(idPost);
        }

        [HttpGet]
        [Route("GetPosts")]
        public IActionResult GetNewPosts(int number, int idDistrict,
            int idCategory, int minPrice, int maxPrice, int minArea,
            int maxArea)
        {
            var p = from m in _context.Motelrooms
                    join posts in _context.Posts on
m.IdRoom equals posts.IdRoom
                    where m.IdCategory == idCategory &&
m.IdDistrict == idDistrict && m.Price >= minPrice && m.Price <= maxPrice &&
m.Area >= minArea && m.Area <= maxArea
                    orderby m.CreatedAt
                    select posts.IdPost;
            var limitPosts = p.Take(number);
            return Ok(limitPosts);
        }

        [HttpPost]
        [Route("PostUp")]
        public IActionResult SendNewPost([FromBody] PostForm post)
        {
            return Ok();
        }
    }
}
