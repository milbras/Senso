using Microsoft.AspNetCore.Mvc;
using Senso.Services;

namespace Senso.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("start")]
        public IActionResult StartGame(int playerId)
        {
            var session = _gameService.StartGame(playerId);
            return Ok(session);
        }

        [HttpGet("{sessionId}/play-sequence")]
        public IActionResult GetSequence(int sessionId)
        {
            var sequence = _gameService.GetSequence(sessionId);
            return Ok(sequence);
        }

        [HttpPost("{sessionId}/input")]
        public IActionResult SubmitInput(int sessionId, int inputColorId)
        {
            var (isCorrect, isCompleted) = _gameService.CheckInput(sessionId, inputColorId);

            if (isCorrect)
            {
                if (isCompleted)
                    return Ok(new { Result = "Correct", NextSequence = true });
                else
                    return Ok(new { Result = "Correct" });
            }
            else
            {
                return Ok(new { Result = "Incorrect" });
            }
        }

        [HttpGet("{sessionId}/score")]
        public IActionResult GetScore(int sessionId)
        {
            var score = _gameService.GetScore(sessionId);
            return Ok(score);
        }
    }
}
