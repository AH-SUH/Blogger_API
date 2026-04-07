using Microsoft.AspNetCore.Mvc;
using REST_API_BloggerAPI.Data;
using REST_API_BloggerAPI.Models;

namespace REST_API_BloggerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        [HttpPost]
        public IActionResult AddPost(Post post)
        {
            var userExists = BlogData.Users.Any(u => u.Id == post.UserId);

            if (!userExists)
                return BadRequest("User does not exist");

            post.Id = BlogData.Posts.Count > 0 ? BlogData.Posts.Max(p => p.Id) + 1 : 1;
            BlogData.Posts.Add(post);

            return Ok(post);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, Post updatedPost)
        {
            var post = BlogData.Posts.FirstOrDefault(p => p.Id == id);

            if (post == null)
                return NotFound();

            post.Title = updatedPost.Title;
            post.Content = updatedPost.Content;
            post.UserId = updatedPost.UserId;

            return Ok(post);
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetPostsByUser(int userId)
        {
            var posts = BlogData.Posts.Where(p => p.UserId == userId).ToList();

            return Ok(posts);
        }
    }
}