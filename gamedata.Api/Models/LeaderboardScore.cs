namespace Score.Models
{
    public class LeaderboardScore
    {
        public int ScoreId { get; set; }
        public int UserId { get; set; }
        public string? Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Game_Score { get; set; }
        public string Game_Version { get; set; } = null!;
    }
}