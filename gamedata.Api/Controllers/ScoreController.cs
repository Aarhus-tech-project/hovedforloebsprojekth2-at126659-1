using Microsoft.AspNetCore.Mvc;
using Score.Models;
using Score.Services;

namespace Score.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoreController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<GameScore>> GetAll() => ScoreService.GetAll();

        [HttpGet]
        [Route("{id}")]
        public ActionResult<GameScore> Get(int id)
        {
            var score = ScoreService.Get(id);
            if (score == null)
                return NotFound();
            return score;
        }

        [HttpPost]
        public ActionResult<GameScore> Post(int userId, int gameScore, string gameVersion)
        {
            var createdScore = ScoreService.Post(userId, gameScore, gameVersion);
            return NoContent();
        }
    }
}