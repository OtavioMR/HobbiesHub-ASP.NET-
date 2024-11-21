using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        // Este é um método de exemplo para verificar o status de login
        [HttpGet("IsLoggedIn")]
        public IActionResult IsLoggedIn()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Ok(new { loggedIn = true });
            }
            else
            {
                return Ok(new { loggedIn = false });
            }
        }
    }
}
