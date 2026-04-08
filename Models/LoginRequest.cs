namespace REST_API_BloggerAPI.Models
{
    public class LoginRequest
    {
        // Username entered by the user during login
        public string Username { get; set; } = string.Empty;

        // Password entered by the user during login
        public string Password { get; set; } = string.Empty;
    }
}