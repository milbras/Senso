using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using Senso.Models;

namespace Senso.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColorController : ControllerBase
    {
        private static readonly List<Models.Color> Colors = new List<Models.Color>
        {
            new Models.Color { Id = 1, Name = "Red" },
            new Models.Color { Id = 2, Name = "Green" },
            new Models.Color { Id = 3, Name = "Blue" },
            new Models.Color { Id = 4, Name = "Yellow" }
        };

        [HttpGet]
        public ActionResult<List<Models.Color>> GetColors()
        {
            return Colors;
        }
    }
}
