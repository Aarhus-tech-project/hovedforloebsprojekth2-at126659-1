using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Score.Models;

namespace Score.Services
{
    public static class ScoreService
    {
        static List<GameScore> Scores { get; }
        static ScoreService()
        {
            Scores = new List<GameScore>
            {
                new GameScore { ScoreId = 1, UserId = 1, CreatedAt = DateTime.UtcNow, Game_Score = 6, Game_Version = "1.0.0" },
                new GameScore { ScoreId = 2, UserId = 2, CreatedAt = DateTime.UtcNow, Game_Score = 7, Game_Version = "1.0.0" }
            };
        }
        public static List<GameScore> GetAll() => Scores;
        public static GameScore? Get(int id) => Scores.FirstOrDefault(s => s.ScoreId == id);
        public static GameScore? Post(int userId, int gameScore, string gameVersion)
        {
            var score = new GameScore { ScoreId = Scores.Count + 1, UserId = userId, CreatedAt = DateTime.UtcNow, Game_Score = gameScore, Game_Version = gameVersion };
            Scores.Add(score);
            return score;
        }
    }
}