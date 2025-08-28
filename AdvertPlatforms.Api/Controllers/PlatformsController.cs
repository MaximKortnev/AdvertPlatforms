using Microsoft.AspNetCore.Mvc;

namespace AdvertPlatforms.Api.Controllers
{
    public class PlatformsController : ControllerBase
    {
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
