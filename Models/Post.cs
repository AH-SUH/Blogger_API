namespace REST_API_BloggerAPI.Models
{
    public class Post
    {
        // Unique ID for each post
        public int Id { get; set; }

        // Title of the blog post
        public string Title { get; set; } = string.Empty;

        // Main content/body of the blog post
        public string Content { get; set; } = string.Empty;

        // ID of the user who created the post
        public int UserId { get; set; }
    }
}