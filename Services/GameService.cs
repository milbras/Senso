using Senso.Models;
using Senso.Repositories;

namespace Senso.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _repo;
        private readonly Random _random = new();

        public GameService(IGameRepository repo)
        {
            _repo = repo;
        }

        public GameSession StartGame(int playerId)
        {
            var session = new GameSession
            {
                Id = _repo.GetSessions().Count + 1,
                PlayerId = playerId,
                Sequence = new List<int> { _random.Next(1, 5) },
                IsActive = true,
                IsGameOver = false,
                CurrentStep = 0
            };

            _repo.AddSession(session);
            return session;
        }

        public List<int> GetSequence(int sessionId)
        {
            var session = _repo.GetSession(sessionId)
                ?? throw new ArgumentException("Session not found");
            return session.Sequence;
        }

        public (bool IsCorrect, bool IsCompleted) CheckInput(int sessionId, int inputColorId)
        {
            var session = _repo.GetSession(sessionId)
                ?? throw new ArgumentException("Session not found");

            if (session.IsGameOver)
                return (false, false);

            if (session.Sequence[session.CurrentStep] == inputColorId)
            {
                session.CurrentStep++;
                if (session.CurrentStep >= session.Sequence.Count)
                {
                    // Complete round, add next
                    session.Sequence.Add(_random.Next(1, 5));
                    session.CurrentStep = 0;
                    return (true, true);
                }
                else
                {
                    return (true, false);
                }
            }
            else
            {
                session.IsGameOver = true;
                session.IsActive = false;
                return (false, false);
            }
        }

        public int GetScore(int sessionId)
        {
            var session = _repo.GetSession(sessionId)
                ?? throw new ArgumentException("Session not found");
            return session.Sequence.Count - 1;
        }
    }
}
