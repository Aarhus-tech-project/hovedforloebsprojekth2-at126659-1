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
    }
}