using Senso.Models;

namespace Senso.Repositories
{
    public interface IGameRepository
    {
        List<Player> GetPlayers();
        Player AddPlayer(string userName);
        List<GameSession> GetSessions();
        GameSession? GetSession(int sessionId);
        void AddSession(GameSession session);
    }
}
