using Microsoft.AspNetCore.Mvc;
using Senso.Models;
using Senso.Repositories;

namespace Senso.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IGameRepository _repo;

        public UserController(IGameRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public ActionResult<Player> Register(string userName)
        {
            var player = _repo.AddPlayer(userName);
            return player;
        }

        [HttpGet]
        public ActionResult<List<Player>> GetPlayers()
        {
            return _repo.GetPlayers();
        }
    }
}
