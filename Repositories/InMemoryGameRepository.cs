using Senso.Models;

namespace Senso.Repositories
{
    public class InMemoryGameRepository : IGameRepository
    {
        private readonly List<Player> _players = new();
        private readonly List<GameSession> _sessions = new();
        private int _nextPlayerId = 1;

        public List<Player> GetPlayers() => _players;

        public Player AddPlayer(string userName)
        {
            var player = new Player
            {
                Id = _nextPlayerId++,
                UserName = userName
            };
            _players.Add(player);
            return player;
        }

        public List<GameSession> GetSessions() => _sessions;

        public GameSession? GetSession(int sessionId)
        {
            return _sessions.FirstOrDefault(s => s.Id == sessionId);
        }

        public void AddSession(GameSession session)
        {
            _sessions.Add(session);
        }
    }
}
