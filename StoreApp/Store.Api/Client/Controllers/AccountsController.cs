using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Store.Api.Client.Controllers
{
    [ApiExplorerSettings(GroupName = "user")]
    [Route("user/api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register()
        {

            return Ok();
        }
    }
}
