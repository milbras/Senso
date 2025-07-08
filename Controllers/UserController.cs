using Microsoft.AspNetCore.Mvc;
using Senso.Models;
using System.Numerics;

namespace Senso.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly List<Player> Players = new List<Player>();
        private static int NextId = 1;

        [HttpPost("register")]
        public ActionResult<Player> Register(string userName)
        {
            var player = new Player { Id = NextId++, UserName = userName };
            Players.Add(player);
            return player;
        }

        [HttpGet]
        public ActionResult<List<Player>> GetPlayers()
        {
            return Players;
        }
    }
}
