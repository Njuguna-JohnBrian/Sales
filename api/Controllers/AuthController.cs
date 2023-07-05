using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("/auth")]
    public class AuthController : ControllerBase
    {
        public AuthController(){}

        [HttpGet]
        public IActionResult Test()
        {
            return Ok(new { message = "App is healthy" });
        }
    }
}