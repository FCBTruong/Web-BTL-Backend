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

                var comments = _context.Comments.Where(c => c.IdPost == post.IdPost && c.Status == 1).ToList();

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
                imagesList = (from imageTbl in _context.RoomImages
                              where imageTbl.IdRoom == motelInfor.IdRoom
                              select imageTbl.ImagePath).ToList();
                int likes = _context.FavoritePosts.Where(f => f.IdPost == idPost).Count();

                postInformation = new PostInformation
                {
                    post = post,
                    owner = owner,
                    motelInfor = motelInfor,
                    comments = postComments,
                    images = imagesList,
                    category = category,
                    likes = likes,
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
            int maxArea, int idUtility)
        {
            if (maxArea == 0) maxArea = 1000000000;
            if (maxPrice == 0) maxPrice = 1000000000;
            if (number == 0) number = 100;
            // var p = null;
            try
            {
                if (idDistrict != 0 && idCategory != 0)
                {
                    var p = (from m in _context.Motelrooms
                             join posts in _context.Posts on
         m.IdRoom equals posts.IdRoom
                             where m.IdCategory == idCategory &&
         m.IdDistrict == idDistrict && m.Price >= minPrice && m.Price <= maxPrice &&
         m.Area >= minArea && m.Area <= maxArea && posts.ExpireDate > DateTime.Today
         && posts.Status == 1
                             orderby posts.CreatedAt
                             select new { posts.IdPost, m.IdUtility }).ToList();
                    if (idUtility != 0)
                    {
                        int j = 0;
                        var listPostId = new List<int>();
                        for (int i = 0; i < p.Count; i++)
                        {
                            var utilities = (Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(p[i].IdUtility));
                            if (utilities.Contains(idUtility))
                            {
                                listPostId.Add(p[i].IdPost);
                                j++;
                                if (j >= number) break;
                            }
                        }

                        return Ok(listPostId);
                    }
                    return Ok(p.Take(number).Select(ps => ps.IdPost));
                }
                else if (idDistrict != 0 && idCategory == 0)
                {
                    var p = (from m in _context.Motelrooms
                             join posts in _context.Posts on
         m.IdRoom equals posts.IdRoom
                             where
         m.IdDistrict == idDistrict && m.Price >= minPrice && m.Price <= maxPrice &&
         m.Area >= minArea && m.Area <= maxArea && posts.ExpireDate > DateTime.Today
          && posts.Status == 1
                             orderby posts.CreatedAt
                             select new { posts.IdPost, m.IdUtility }).ToList();

                    if (idUtility != 0)
                    {
                        int j = 0;
                        var listPostId = new List<int>();
                        for (int i = 0; i < p.Count; i++)
                        {
                            var utilities = (Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(p[i].IdUtility));
                            if (utilities.Contains(idUtility))
                            {
                                listPostId.Add(p[i].IdPost);
                                j++;
                                if (j >= number) break;
                            }
                        }

                        return Ok(listPostId);
                    }
                    return Ok(p.Take(number).Select(ps => ps.IdPost));
                }
                else if (idDistrict == 0 && idCategory != 0)
                {
                    var p = (from m in _context.Motelrooms
                             join posts in _context.Posts on
         m.IdRoom equals posts.IdRoom
                             where m.IdCategory == idCategory && m.Price >= minPrice && m.Price <= maxPrice &&
         m.Area >= minArea && m.Area <= maxArea && posts.ExpireDate > DateTime.Today
          && posts.Status == 1
                             orderby posts.CreatedAt
                             select new { posts.IdPost, m.IdUtility }).ToList();

                    if (idUtility != 0)
                    {
                        int j = 0;
                        var listPostId = new List<int>();
                        for (int i = 0; i < p.Count; i++)
                        {
                            var utilities = (Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(p[i].IdUtility));
                            if (utilities.Contains(idUtility))
                            {
                                listPostId.Add(p[i].IdPost);
                                j++;
                                if (j >= number) break;
                            }
                        }

                        return Ok(listPostId);
                    }
                    return Ok(p.Take(number).Select(ps => ps.IdPost));
                }
                else
                {
                    var p = (from m in _context.Motelrooms
                             join posts in _context.Posts on
         m.IdRoom equals posts.IdRoom
                             where m.Price >= minPrice && m.Price <= maxPrice &&
         m.Area >= minArea && m.Area <= maxArea && posts.ExpireDate > DateTime.Today
          && posts.Status == 1
                             orderby posts.CreatedAt
                             select new { posts.IdPost, m.IdUtility }).ToList();

                    if (idUtility != 0)
                    {
                        int j = 0;
                        var listPostId = new List<int>();
                        for (int i = 0; i < p.Count; i++)
                        {
                            var utilities = (Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(p[i].IdUtility));
                            if (utilities.Contains(idUtility))
                            {
                                listPostId.Add(p[i].IdPost);
                                j++;
                                if (j >= number) break;
                            }
                        }

                        return Ok(listPostId);
                    }
                    return Ok(p.Take(number).Select(ps => ps.IdPost));
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error" + e);
            }
        }

        [Authorize]

        [Route("PostUp")]
        public IActionResult SendNewPost([FromForm] String postStr, [FromForm] List<IFormFile> files)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();

                if (claim[1].Value == "user") return Unauthorized();
                if (claim[1].Value == "owner")
                {
                    PostForm post = JsonConvert.DeserializeObject<PostForm>(postStr.ToString());

                    var slug = Slugify.CreateSlugify(post.title);
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
                        IdUtility = '[' + string.Join(",", post.utilities.ToArray()) + ']',
                        Slug = "",
                        Status = 0,
                    };

                    _context.Motelrooms.Add(newMotelRoom);
                    _context.SaveChanges();

                    // save to database
                    Posts newPost = new Posts
                    {
                        IdPost = newMotelRoom.IdRoom,
                        IdRoom = newMotelRoom.IdRoom,
                        IdUser = Int32.Parse(claim[2].Value),
                        Status = 0, // pending
                        CreatedAt = DateTime.Now,
                        ExpireDate = DateTime.Now.AddDays(10),
                        // Expire in 10 days.
                        UpdatedAt = DateTime.Now,
                        PacketType = post.packetType,
                        PacketValue = post.packetValue
                    };
                    newMotelRoom.Slug = slug + "-id-" + newMotelRoom.IdRoom;

                    for (int i = 0; i < files.Count; i++)
                    {
                        _context.RoomImages.Add(
                            new RoomImages
                            {
                                IdRoom = newMotelRoom.IdRoom,
                                ImagePath = i.ToString() + ".jpg"
                            });
                    }
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
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }

        [HttpGet]
        [Route("GetPostWithCategory")]
        public IActionResult GetPostWithCategory(string slug, int number)
        {
            if (number == 0) number = 10;
            var p = from m in _context.Motelrooms
                    join posts in _context.Posts
on m.IdRoom equals posts.IdRoom
                    join categories in _context.Categories on
                    m.IdCategory equals categories.IdCategory
                    where categories.Slug == slug && posts.Status == 1 && posts.ExpireDate > DateTime.Today
                    orderby posts.CreatedAt
                    select posts.IdPost;

            var limitPosts = p.Take(number);
            return Ok(limitPosts);
        }

        [Authorize]
        [HttpGet]
        [Route("GetPostsPending")]
        public IActionResult GetPostsPending()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();

                if (claim[1].Value != "admin") return Unauthorized();

                var listPostsPending = from posts in _context.Posts
                                       where
          posts.Status == 0
                                       select posts.IdPost;
                return Ok(listPostsPending);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("AcceptPost")]
        public IActionResult AcceptPost(int idPost)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();

                if (claim[1].Value != "admin") return Unauthorized();

                var post = _context.Posts.Find(idPost);
                post.Status = 1; // accept

                int expireDays = 0;
                switch (post.PacketType)
                {
                    case 1:
                        expireDays = 7 * (post.PacketValue).GetValueOrDefault();
                        break;
                    case 2:
                        expireDays = 30 * (post.PacketValue).GetValueOrDefault();
                        break;
                    case 3:
                        expireDays = 90 * (post.PacketValue).GetValueOrDefault();
                        break;
                    case 4:
                        expireDays = 360 * (post.PacketValue).GetValueOrDefault();
                        break;
                }

                post.ExpireDate = DateTime.Today.AddDays(expireDays);
                _context.SaveChanges();

                return Ok("Accepted this post!");
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("RemovePost")]
        public IActionResult RemovePost(int idPost)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();

                if (claim[1].Value != "admin") return Unauthorized();

                //var post = _context.Posts.Find(idPost);
                //post.Status = 1; // accept

                // _context.RoomImages.Remove
                _context.SaveChanges();

                return Ok("Accepted this post!");
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetPostOfUser")]
        public IActionResult GetPostOfUser()
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();

                if (claim[1].Value == "user") return Unauthorized();
                var idUser = Int32.Parse(claim[2].Value);
                var listIdPosts = (from post in _context.Posts
                                   where post.IdUser == idUser
                                   select post.IdPost).ToList();

                List<PostInformation> listPosts = new List<PostInformation>();

                for (int i = 0; i < listIdPosts.Count; i++)
                {
                    listPosts.Add(GetPostPrivate(listIdPosts[i]));
                }

                return Ok(listPosts);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e);
            }
        }

        PostInformation GetPostPrivate(int idPost)
        {
            PostInformation postInformation;

            var posts = (_context.Posts.Where(p => p.IdPost == idPost).ToList());
            if (posts == null || posts.Count == 0) return null;
            Posts post = posts[0];

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
            imagesList = (from imageTbl in _context.RoomImages
                          where imageTbl.IdRoom == motelInfor.IdRoom
                          select imageTbl.ImagePath).ToList();
            int likes = _context.FavoritePosts.Where(f => f.IdPost == idPost).Count();
            postInformation = new PostInformation
            {
                post = post,
                owner = owner,
                motelInfor = motelInfor,
                comments = postComments,
                images = imagesList,
                category = category,
                likes = likes
            };
            // conn.Close();
            return postInformation;
        }

        [HttpGet]
        [Route("GetPostsWithRange")]
        public IActionResult GetPostsWithRange(int startPoint, int number)
        {
            try
            {
           
                var posts = _context.Posts.Where(p => p.Status == 1).
                    OrderBy(
                    p => p.CreatedAt).ToList();
                var idList = new List<int>();

                if(posts.Count >= startPoint)
                {
                    int j = 0;
                    for(int i = startPoint; i <= posts.Count; i++)
                    {
                        idList.Add(posts[i - 1].IdPost);
                        j++;
                        if (j > number) break;
                    }
                }
                return Ok(idList);
            }
            catch (Exception e)
            {
                return BadRequest("Error" + e);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("AdminGetAllPosts")]
        public IActionResult AdminGetAllPosts(int number)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();

                if (claim[1].Value != "admin") return Unauthorized();

                if (number == 0) number = _context.Posts.ToList().Count;
                var listPosts = (from post in _context.Posts
                                 join room in _context.Motelrooms
                                 on post.IdRoom equals room.IdRoom
                                 select (new
                                 {
                                     post.IdPost,
                                     post.Status,
                                     post.CreatedAt,
                                     post.ExpireDate,
                                     room.Price,
                                     room.Slug,
                                     room.Title,
                                     room.IdCategory,
                                     post.IdUser
                                 })
                                   ).Take(number).ToList();


                return Ok(listPosts);
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("AdminGetPostWithId")]
        public IActionResult AmdinGetPostWithId(int idPost)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;
                IList<Claim> claim = identity.Claims.ToList();

                if (claim[1].Value != "admin") return Unauthorized();

                return Ok(GetPostPrivate(idPost));
            }
            catch (Exception e)
            {
                return BadRequest("Error: " + e);
            }
        }
    }
}
