using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using REST_API_BloggerAPI.Data;
using REST_API_BloggerAPI.Models;

namespace REST_API_BloggerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // Create a new user
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            user.Id = BlogData.Users.Count > 0
                ? BlogData.Users.Max(u => u.Id) + 1
                : 1;

            BlogData.Users.Add(user);

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new
            {
                user.Id,
                user.Name,
                user.Username,
                user.Role
            });
        }

        // Get all users
        [Authorize]
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = BlogData.Users.Select(u => new
            {
                u.Id,
                u.Name,
                u.Username,
                u.Role
            });

            return Ok(users);
        }

        // Get a single user by ID
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = BlogData.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound("User not found.");

            return Ok(new
            {
                user.Id,
                user.Name,
                user.Username,
                user.Role
            });
        }

        // Update an existing user by ID
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            var user = BlogData.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound("User not found.");

            user.Name = updatedUser.Name;
            user.Username = updatedUser.Username;
            user.Password = updatedUser.Password;
            user.Role = updatedUser.Role;

            return Ok(new
            {
                Message = "User updated successfully.",
                User = new
                {
                    user.Id,
                    user.Name,
                    user.Username,
                    user.Role
                }
            });
        }

        // Delete a user by ID
        // Only users with the ADMIN role are allowed to delete users
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = BlogData.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound("User not found.");

            BlogData.Users.Remove(user);

            return Ok(new { Message = "User deleted successfully." });
        }
    }
}