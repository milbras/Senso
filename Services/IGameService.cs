using Senso.Models;

namespace Senso.Services
{
    public interface IGameService
    {
        GameSession StartGame(int playerId);
        List<int> GetSequence(int sessionId);
        (bool IsCorrect, bool IsCompleted) CheckInput(int sessionId, int inputColorId);
        int GetScore(int sessionId);
    }
}
