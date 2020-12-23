using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Web_BTL_Backend.Models;
using Web_BTL_Backend.Models.ClientDataReturn;
using Web_BTL_Backend.Models.ClientSendForm;
using Web_BTL_Backend.Models.Data;

namespace Web_BTL_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        //  public MySqlConnection conn;
        private IConfiguration _config;
        public db_a6a86f_truongContext _context;

        public object Configuration { get; }

        public PostController(IConfiguration config, db_a6a86f_truongContext dbContext)
        {
            this._config = config;
            this._context = dbContext;

            try
            {
                /* conn = new MySql.Data.MySqlClient.MySqlConnection();
                 conn.ConnectionString = config["ConnectionStrings:btl_webContext"];
                 conn.Open();*/
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

            }
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
                List<string> imagesList = new List<string>();
                //  if (conn.State == System.Data.ConnectionState.Closed) conn.Open();
                /*
                                string sql = "Select image_path from room_images where id_room=" + post.IdRoom.ToString();
                                MySqlCommand cmd = new MySqlCommand(sql, conn);
                                MySqlDataReader rdr = cmd.ExecuteReader();

                                while (rdr.Read())
                                {
                                    imagesList.Add(rdr.GetString("image_path"));
                                };

                                rdr.Close();*/

                postInformation = new PostInformation
                {
                    post = post,
                    owner = owner,
                    motelInfor = motelInfor,
                    comments = postComments,
                    images = imagesList,
                    category = category,
                };
                // conn.Close();
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
            if (maxArea == 0) maxArea = 1000000000;
            if (maxPrice == 0) maxPrice = 1000000000;
            if (number == 0) number = 100;

            if (idDistrict != 0 && idCategory != 0)
            {
                var p = from m in _context.Motelrooms
                        join posts in _context.Posts on
    m.IdRoom equals posts.IdRoom
                        where m.IdCategory == idCategory &&
    m.IdDistrict == idDistrict && m.Price >= minPrice && m.Price <= maxPrice &&
    m.Area >= minArea && m.Area <= maxArea && posts.ExpireDate > DateTime.Today
                        orderby posts.CreatedAt
                        select posts.IdPost;

                var limitPosts = p.Take(number);
                return Ok(limitPosts);
            }
            else if (idDistrict != 0 && idCategory == 0)
            {
                var p = from m in _context.Motelrooms
                        join posts in _context.Posts on
    m.IdRoom equals posts.IdRoom
                        where
    m.IdDistrict == idDistrict && m.Price >= minPrice && m.Price <= maxPrice &&
    m.Area >= minArea && m.Area <= maxArea && posts.ExpireDate > DateTime.Today
                        orderby posts.CreatedAt
                        select posts.IdPost;

                var limitPosts = p.Take(number);
                return Ok(limitPosts);
            }
            else if (idDistrict == 0 && idCategory != 0)
            {
                var p = from m in _context.Motelrooms
                        join posts in _context.Posts on
    m.IdRoom equals posts.IdRoom
                        where m.IdCategory == idCategory && m.Price >= minPrice && m.Price <= maxPrice &&
    m.Area >= minArea && m.Area <= maxArea && posts.ExpireDate > DateTime.Today
                        orderby posts.CreatedAt
                        select posts.IdPost;

                var limitPosts = p.Take(number);
                return Ok(limitPosts);
            }
            else
            {
                var p = from m in _context.Motelrooms
                        join posts in _context.Posts on
    m.IdRoom equals posts.IdRoom
                        where m.Price >= minPrice && m.Price <= maxPrice &&
    m.Area >= minArea && m.Area <= maxArea && posts.ExpireDate > DateTime.Today
                        orderby posts.CreatedAt
                        select posts.IdPost;

                var limitPosts = p.Take(number);
                return Ok(limitPosts);
            }
            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        [Route("PostUp")]
        public IActionResult SendNewPost([FromForm] String postStr, [FromForm] List<IFormFile> files)
        {
            // try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();

                if (claim[1].Value == "user") return Unauthorized();
                if (claim[1].Value == "owner")
                {
                    PostForm post = JsonConvert.DeserializeObject<PostForm>(postStr.ToString());

                    Motelrooms newMotelRoom = new Motelrooms
                    {
                        Title = post.title,
                        Address = post.address,
                        Position = JsonConvert.SerializeObject(new Position(post.lat, post.lng)),
                        Price = post.price,
                        Area = post.area,
                        IdDistrict = post.district,
                        IdCategory = post.category,
                        Phone = post.phone,
                        Description = post.description,
                    };

                    _context.Motelrooms.Add(newMotelRoom);
                    _context.SaveChanges();

                    // save to database
                    Posts newPost = new Posts
                    {
                        IdPost = newMotelRoom.IdRoom,
                        IdRoom = newMotelRoom.IdRoom,
                        IdUser = Int32.Parse(claim[0].Value),
                        Status = 0, // pending
                        CreatedAt = DateTime.Today,
                        ExpireDate = DateTime.Today.AddDays(10),
                        // Expire in 10 days.
                        UpdatedAt = DateTime.Today
                    };
                    _context.Posts.Add(newPost);
                    _context.SaveChanges();

                    // save Image
                    var idPost = newMotelRoom.IdRoom;
                    var _f = new FileServices();
                    var subPath = "posts/" + idPost.ToString();
                    _f.SaveFile(files, subPath);

                    return Ok(post);
                }
                return Unauthorized();
            }
            /* catch (Exception exception)
             {
                 return BadRequest($"Error: {exception.Message}");
             }*/
        }

        [HttpGet]
        [Route("GetPostWithCategory")]
        public IActionResult GetPostWithCategory(int idCategory, int number)
        {
            if (number == 0) number = 10;
            var p = from m in _context.Motelrooms
                    join posts in _context.Posts
on m.IdRoom equals posts.IdRoom
                    where m.IdCategory == idCategory && posts.ExpireDate > DateTime.Today
                    orderby posts.CreatedAt
                    select posts.IdPost;

            var limitPosts = p.Take(number);
            return Ok(limitPosts);
        }
    }
}
