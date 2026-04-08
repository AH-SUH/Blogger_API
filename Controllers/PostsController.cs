using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using REST_API_BloggerAPI.Data;
using REST_API_BloggerAPI.Models;

namespace REST_API_BloggerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        // Adds a new post
        // User must be logged in to create a post
        [Authorize]
        [HttpPost]
        public IActionResult AddPost(Post post)
        {
            // Check whether the userId on the post actually exists
            var userExists = BlogData.Users.Any(u => u.Id == post.UserId);

            // If the user does not exist, return a 400 Bad Request
            if (!userExists)
                return BadRequest("Invalid userId. User does not exist.");

            // Generate a new ID for the post
            post.Id = BlogData.Posts.Count > 0 ? BlogData.Posts.Max(p => p.Id) + 1 : 1;

            // Add the post to the list
            BlogData.Posts.Add(post);

            // Return the new post
            return Ok(post);
        }

        // Updates an existing post by ID
        [HttpPut("{id}")]
        public IActionResult UpdatePost(int id, Post updatedPost)
        {
            // Find the post by ID
            var post = BlogData.Posts.FirstOrDefault(p => p.Id == id);

            // If not found, return 404
            if (post == null)
                return NotFound("Post not found.");

            // Check whether the updated post still has a valid userId
            var userExists = BlogData.Users.Any(u => u.Id == updatedPost.UserId);

            if (!userExists)
                return BadRequest("Invalid userId. User does not exist.");

            // Update the post values
            post.Title = updatedPost.Title;
            post.Content = updatedPost.Content;
            post.UserId = updatedPost.UserId;

            // Return the updated post
            return Ok(post);
        }

        // Returns all posts for a specific user ID
        [HttpGet("user/{userId}")]
        public IActionResult GetPostsByUserId(int userId)
        {
            // Filter posts to only those that match the given userId
            var posts = BlogData.Posts.Where(p => p.UserId == userId).ToList();

            // Return matching posts as JSON
            return Ok(posts);
        }
    }
}