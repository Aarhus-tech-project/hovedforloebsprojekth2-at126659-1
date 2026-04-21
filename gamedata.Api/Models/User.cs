namespace User.Models
{
    public class AppUser
    {
        public int Id { get; set; }

        //attributes
        public string Username { get; set; } = null!;
        public int? Avatar { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; } = null!;
    }
}