using Microsoft.AspNetCore.Mvc;
using Score.Models;
using Score.Services;
using User.Services;

namespace Leaderboard.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaderboardController : ControllerBase
    {
        //Get Leaderboard Highscores
        [HttpGet]
        public ActionResult<List<LeaderboardScore>> GetAll(
            int count,
            bool allowDuplicatesUsers = false,
            string? gameVersion = null,
            LeaderboardTimeSlot timeSlot = LeaderboardTimeSlot.AllTime)
        {
            if (count <= 0)
                return BadRequest("Cannot request less than 1 Score");
            if (ScoreService.GetAll().Count == 0)
                return NoContent();

            var scores = ScoreService.GetAll(count, allowDuplicatesUsers, gameVersion, timeSlot);
            if (scores.Count == 0)
                return NoContent();

            if (!string.IsNullOrWhiteSpace(gameVersion)
                && !ScoreService.GetAll().Any(s => s.Game_Version == gameVersion))
            {
                return NotFound("No scores found for game version " + gameVersion);
            }

            var result = scores
                .Select(s => new LeaderboardScore
                {
                    ScoreId = s.ScoreId,
                    UserId = s.UserId,
                    Username = UserService.Get(s.UserId)?.Username,
                    CreatedAt = s.CreatedAt,
                    Game_Score = s.Game_Score,
                    Game_Version = s.Game_Version
                })
                .ToList();

            return result;
        }
    }
}