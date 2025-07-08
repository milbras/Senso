using Microsoft.AspNetCore.Mvc;
using Senso.Models;

namespace Senso.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private static readonly List<GameSession> Sessions = new List<GameSession>();
        private static readonly Random Random = new Random();
        private static int NextId = 1;

        private static readonly List<int> ColorIds = new List<int> { 1, 2, 3, 4 };

        [HttpPost("start")]
        public ActionResult<GameSession> StartGame(int playerId)
        {
            var session = new GameSession
            {
                Id = NextId++,
                PlayerId = playerId,
                Sequence = new List<int> { Random.Next(1, 5) },
                IsActive = true
            };
            Sessions.Add(session);
            return session;
        }

        [HttpGet("{sessionId}")]
        public ActionResult<GameSession> GetSession(int sessionId)
        {
            var session = Sessions.FirstOrDefault(s => s.Id == sessionId);
            if (session == null) return NotFound();
            return session;
        }

        [HttpPost("{sessionId}/submit")]
        public ActionResult SubmitSequence(int sessionId, [FromBody] List<int> playerSequence)
        {
            var session = Sessions.FirstOrDefault(s => s.Id == sessionId);
            if (session == null || !session.IsActive) return BadRequest("Invalid session");

            // Validate input
            for (int i = 0; i < session.Sequence.Count; i++)
            {
                if (i >= playerSequence.Count || session.Sequence[i] != playerSequence[i])
                {
                    session.IsActive = false;
                    return Ok(new { Result = "Incorrect", Sequence = session.Sequence });
                }
            }

            // Add new color
            session.Sequence.Add(Random.Next(1, 5));
            return Ok(new { Result = "Correct", NewSequence = session.Sequence });
        }
    }
}
