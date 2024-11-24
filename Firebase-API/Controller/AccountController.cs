using Microsoft.AspNetCore.Mvc;

namespace Firebase_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpGet("IsLoggedIn")]
        public IActionResult IsLoggedIn()
        {
            return Ok(new { loggedIn = true });
        }
    }
}
