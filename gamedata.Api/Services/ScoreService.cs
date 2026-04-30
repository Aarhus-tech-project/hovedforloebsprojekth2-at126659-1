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
                new GameScore { ScoreId = 1, UserId = 1, CreatedAt = DateTime.UtcNow.AddDays(-67), Game_Score = 6, Game_Version = "1.0.0" },
                new GameScore { ScoreId = 2, UserId = 1, CreatedAt = DateTime.UtcNow.AddDays(-12), Game_Score = 5, Game_Version = "1.0.0" },
                new GameScore { ScoreId = 3, UserId = 1, CreatedAt = DateTime.UtcNow, Game_Score = 5, Game_Version = "1.0.1" },
                new GameScore { ScoreId = 4, UserId = 1, CreatedAt = DateTime.UtcNow, Game_Score = 6, Game_Version = "1.0.1" },
                new GameScore { ScoreId = 5, UserId = 2, CreatedAt = DateTime.UtcNow.AddDays(-6), Game_Score = 7, Game_Version = "1.0.0" },
                new GameScore { ScoreId = 6, UserId = 2, CreatedAt = DateTime.UtcNow.AddDays(-2), Game_Score = 4, Game_Version = "1.0.0" },
                new GameScore { ScoreId = 7, UserId = 2, CreatedAt = DateTime.UtcNow, Game_Score = 9, Game_Version = "1.0.1" },
                new GameScore { ScoreId = 8, UserId = 2, CreatedAt = DateTime.UtcNow, Game_Score = 7, Game_Version = "1.0.2" },
                new GameScore { ScoreId = 9, UserId = 3, CreatedAt = DateTime.UtcNow.AddDays(-29), Game_Score = 10, Game_Version = "1.0.0" },
                new GameScore { ScoreId = 10, UserId = 3, CreatedAt = DateTime.UtcNow.AddDays(-9), Game_Score = 8, Game_Version = "1.0.1" },
                new GameScore { ScoreId = 11, UserId = 3, CreatedAt = DateTime.UtcNow, Game_Score = 6, Game_Version = "1.0.2" },
                new GameScore { ScoreId = 12, UserId = 4, CreatedAt = DateTime.UtcNow.AddDays(-1), Game_Score = 4, Game_Version = "1.0.2" },
                new GameScore { ScoreId = 13, UserId = 4, CreatedAt = DateTime.UtcNow, Game_Score = 9, Game_Version = "1.0.2" },
                new GameScore { ScoreId = 14, UserId = 4, CreatedAt = DateTime.UtcNow, Game_Score = 8, Game_Version = "1.0.2" },
                new GameScore { ScoreId = 15, UserId = 5, CreatedAt = DateTime.UtcNow.AddDays(-17), Game_Score = 10, Game_Version = "1.0.0" },
                new GameScore { ScoreId = 16, UserId = 5, CreatedAt = DateTime.UtcNow.AddDays(-11), Game_Score = 9, Game_Version = "1.0.0" },
                new GameScore { ScoreId = 17, UserId = 5, CreatedAt = DateTime.UtcNow.AddDays(-11), Game_Score = 11, Game_Version = "1.0.0" }
            };
        }
        public static List<GameScore> GetAll() => Scores;
        public static List<GameScore> GetAll(
            int count,
            bool allowDuplicatesUsers = false,
            string? gameVersion = null,
            LeaderboardTimeSlot timeSlot = LeaderboardTimeSlot.AllTime)
        {
            var filteredScores = Scores.AsEnumerable();
            var timeWindowStart = GetTimeWindowStart(timeSlot);

            if (timeWindowStart.HasValue)
            {
                filteredScores = filteredScores.Where(s => s.CreatedAt >= timeWindowStart.Value);
            }

            if (!string.IsNullOrWhiteSpace(gameVersion))
            {
                filteredScores = filteredScores.Where(s => s.Game_Version == gameVersion);
            }

            if (!allowDuplicatesUsers)
            {
                // Keep only each user's best score.
                filteredScores = filteredScores
                    .GroupBy(s => s.UserId)
                    .Select(g => g
                        .OrderByDescending(s => s.Game_Score)
                        .ThenByDescending(s => s.CreatedAt)
                        .First());
            }

            return filteredScores
                .OrderByDescending(s => s.Game_Score)
                .ThenByDescending(s => s.CreatedAt)
                .Take(count)
                .ToList();
        }

        private static DateTime? GetTimeWindowStart(LeaderboardTimeSlot timeSlot)
        {
            return timeSlot switch
            {
                LeaderboardTimeSlot.AllTime => null,
                LeaderboardTimeSlot.Week => DateTime.UtcNow.AddDays(-7),
                LeaderboardTimeSlot.Month => DateTime.UtcNow.AddMonths(-1),
                LeaderboardTimeSlot.Year => DateTime.UtcNow.AddYears(-1),
                _ => null
            };
        }
        
        public static List<GameScore> GetTop(int count) => Scores
            .OrderByDescending(s => s.Game_Score)
            .Take(count)
            .ToList();
        public static GameScore? Get(int id) => Scores.FirstOrDefault(s => s.ScoreId == id);
        public static GameScore? Post(int userId, int gameScore, string gameVersion)
        {
            var score = new GameScore { ScoreId = Scores.Count + 1, UserId = userId, CreatedAt = DateTime.UtcNow, Game_Score = gameScore, Game_Version = gameVersion };
            Scores.Add(score);
            return score;
        }
        public static GameScore? Delete(int id)
        {
            var score = Get(id);
            if (score != null)
            {
                Scores.Remove(score);
            }
            return score;
        }
    }
}