using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using Web_BTL_Backend.Models.ClientDataReturn;
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
                };

                return Ok(postInformation);
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        [HttpGet]
        [Route("GetPosts")]
        public IActionResult GetPosts(int number)
        {
            try
            {
                List<int> postsId = new List<int>();

                var list = _context.Posts.ToList();

                int n = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].Status == 0) continue;
                    if (list[i].ExpireDate < DateTime.Today.Date)
                    {
                        continue;
                    }
                    postsId.Add(list[i].IdPost);
                    n++;
                    if (n > number) break;
                }

                return Ok(postsId);
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }
    }
}
