using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KriniteAuthServer.ResourceDataAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuthenticationController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var claims = User.Claims.Select(claim => new { claim.Type, claim.Value });
        return Ok(claims);
    }
}
