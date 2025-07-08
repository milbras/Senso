using Senso.Models;
using Senso.Repositories;
using Senso.Models;
using System.Collections.Generic;
using System.Linq;

namespace Senso.Repositories
{
    public class InMemoryGameRepository : IGameRepository
    {
        private readonly List<Player> _players = new();
        private readonly List<GameSession> _sessions = new();
        private int _nextPlayerId = 1;
        private int _nextSessionId = 1;
        private readonly Random _random = new();

        public List<Player> GetPlayers() => _players;

        public Player AddPlayer(string userName)
        {
            var player = new Player { Id = _nextPlayerId++, UserName = userName };
            _players.Add(player);
            return player;
        }

        public List<GameSession> GetSessions() => _sessions;

        public GameSession StartSession(int playerId)
        {
            var session = new GameSession
            {
                Id = _nextSessionId++,
                PlayerId = playerId,
                Sequence = new List<int> { _random.Next(1, 5) },
                IsActive = true
            };
            _sessions.Add(session);
            return session;
        }

        public GameSession? GetSession(int sessionId)
        {
            return _sessions.FirstOrDefault(s => s.Id == sessionId);
        }

        public void UpdateSession(GameSession session)
        {
            // In-memory list reference already updated.
        }
    }
}
