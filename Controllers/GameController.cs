using Microsoft.AspNetCore.Mvc;
using Senso.Models;
using Senso.Repositories;

namespace Senso.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _repo;
        private readonly Random _random = new();

        public GameController(IGameRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Starts a new game session.
        /// </summary>
        [HttpPost("start")]
        public ActionResult<GameSession> StartGame(int playerId)
        {
            var session = new GameSession
            {
                Id = _repo.GetSessions().Count + 1,
                PlayerId = playerId,
                Sequence = new List<int>(),
                IsActive = true,
                CurrentStep = 0,
                IsGameOver = false
            };

            GenerateNextStep(session);
            _repo.GetSessions().Add(session);
            return session;
        }

        /// <summary>
        /// Generates the next color in the sequence.
        /// </summary>
        private void GenerateNextStep(GameSession session)
        {
            var nextColorId = _random.Next(1, 5);
            session.Sequence.Add(nextColorId);
        }

        /// <summary>
        /// Returns the current sequence to play back to the player.
        /// </summary>
        [HttpGet("{sessionId}/play-sequence")]
        public ActionResult<List<int>> PlaySequence(int sessionId)
        {
            var session = _repo.GetSession(sessionId);
            if (session == null || session.IsGameOver)
                return BadRequest("Invalid or finished game.");

            return session.Sequence;
        }

        /// <summary>
        /// Checks the next input from the player.
        /// </summary>
        [HttpPost("{sessionId}/input")]
        public ActionResult CheckInput(int sessionId, int inputColorId)
        {
            var session = _repo.GetSession(sessionId);
            if (session == null || session.IsGameOver)
                return BadRequest("Invalid or finished game.");

            // Check the current step
            if (session.Sequence[session.CurrentStep] == inputColorId)
            {
                session.CurrentStep++;

                // Check if sequence completed
                if (session.CurrentStep >= session.Sequence.Count)
                {
                    // Add next step
                    GenerateNextStep(session);
                    session.CurrentStep = 0;
                    return Ok(new { Result = "Correct", NextSequenceLength = session.Sequence.Count });
                }
                else
                {
                    return Ok(new { Result = "Correct", Step = session.CurrentStep });
                }
            }
            else
            {
                EndGame(session);
                return Ok(new { Result = "Incorrect", Score = GetScore(session) });
            }
        }

        /// <summary>
        /// Ends the game.
        /// </summary>
        private void EndGame(GameSession session)
        {
            session.IsGameOver = true;
            session.IsActive = false;
        }

        /// <summary>
        /// Returns the player's score (sequence length - 1).
        /// </summary>
        [HttpGet("{sessionId}/score")]
        public ActionResult<int> GetScore(int sessionId)
        {
            var session = _repo.GetSession(sessionId);
            if (session == null)
                return NotFound();

            return GetScore(session);
        }

        private int GetScore(GameSession session)
        {
            // Score = number of successfully completed rounds
            return session.Sequence.Count - 1;
        }
    }
}
