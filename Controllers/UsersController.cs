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
        // Adds a new user
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            // Generate a new ID by finding the highest existing ID and adding 1
            user.Id = BlogData.Users.Count > 0 ? BlogData.Users.Max(u => u.Id) + 1 : 1;

            // Add the new user to the list
            BlogData.Users.Add(user);

            // Return the newly created user as JSON
            return Ok(user);
        }

        // Updates an existing user by ID
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            // Find the user that matches the route ID
            var user = BlogData.Users.FirstOrDefault(u => u.Id == id);

            // If no user is found, return 404
            if (user == null)
                return NotFound("User not found.");

            // Update the user's values
            user.Name = updatedUser.Name;
            user.Username = updatedUser.Username;
            user.Password = updatedUser.Password;
            user.Role = updatedUser.Role;

            // Return the updated user
            return Ok(user);
        }

        // Deletes a user by ID
        // Only users with the ADMIN role are allowed to do this
        [Authorize(Roles = "ADMIN")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            // Find the user to delete
            var user = BlogData.Users.FirstOrDefault(u => u.Id == id);

            // If user does not exist, return 404
            if (user == null)
                return NotFound("User not found.");

            // Remove the user from the list
            BlogData.Users.Remove(user);

            // Return confirmation message
            return Ok("User deleted successfully.");
        }
    }
}