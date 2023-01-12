using KriniteAuthServer.ResourceDataAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace KriniteAuthServer.ResourceDataAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplaintController : ControllerBase
{
    public ComplaintController() { }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComplaintModel>>> GetComplaintsAsync()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ComplaintModel>> GetComplaintAsync(Guid id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddComplaintAsync()
    {
        return CreatedAtAction(nameof(GetComplaintAsync), Guid.NewGuid());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComplaintAsync(Guid id)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveComplaintAsync(Guid id)
    {
        return Ok();
    }

}
