namespace Score.Models
{
    public class GameScore
    {
        public int ScoreId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //attributes
        public int Game_Score { get; set; } = 0;
        public string Game_Version { get; set; } = null!;
    }
}