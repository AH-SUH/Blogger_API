using Microsoft.AspNetCore.Mvc;
using REST_API_BloggerAPI.Data;
using REST_API_BloggerAPI.Models;

namespace REST_API_BloggerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(BlogData.Users);
        }
        [HttpPost]

        public IActionResult AddUser(User user)
        {
            user.Id = BlogData.Users.Count > 0 ? BlogData.Users.Max(u => u.Id) + 1 : 1;
            BlogData.Users.Add(user);

            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            var user = BlogData.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            user.Name = updatedUser.Name;

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = BlogData.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
                return NotFound();

            BlogData.Users.Remove(user);

            return Ok("User deleted");
        }
    }
}