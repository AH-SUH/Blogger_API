using REST_API_BloggerAPI.Models;

namespace REST_API_BloggerAPI.Data
{
    public static class BlogData
    {
        // In-memory list of users for testing the API
        // This acts like a fake database for now
        public static List<User> Users = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "Admin User",
                Username = "admin",
                Password = "admin123",
                Role = "ADMIN"
            },
            new User
            {
                Id = 2,
                Name = "Regular User",
                Username = "user",
                Password = "user123",
                Role = "USER"
            }
        };

        // In-memory list of posts for testing
        public static List<Post> Posts = new List<Post>();
    }
}