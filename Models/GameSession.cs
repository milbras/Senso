namespace Senso.Models
{
    public class GameSession
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public List<int> Sequence { get; set; } = new List<int>();
        public bool IsActive { get; set; } = true;
    }
}
