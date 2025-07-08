using Senso.Models;

namespace Senso.Repositories
{
    public interface IGameRepository
    {
        List<Player> GetPlayers();
        Player AddPlayer(string userName);
        List<GameSession> GetSessions();
        GameSession StartSession(int playerId);
        GameSession? GetSession(int sessionId);
        void UpdateSession(GameSession session);
    }
}
