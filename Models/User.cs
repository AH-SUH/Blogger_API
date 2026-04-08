namespace REST_API_BloggerAPI.Models
{
    public class User
    {
        // Unique ID for each user
        public int Id { get; set; }

        // Display name for the user
        public string Name { get; set; } = string.Empty;

        // Username used for login
        public string Username { get; set; } = string.Empty;

        // Password used for login
        // Note: In a real application, passwords should be hashed, not stored as plain text
        public string Password { get; set; } = string.Empty;

        // Role determines what the user is allowed to do
        // Example roles: ADMIN, USER
        public string Role { get; set; } = "USER";
    }
}