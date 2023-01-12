using KriniteAuthServer.ResourceDataAPI.Models;
using KriniteAuthServer.ResourceDataAPI.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace KriniteAuthServer.ResourceDataAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplaintController : ControllerBase
{
    private readonly IComplaintRepository _complaintRepository;

    public ComplaintController(IComplaintRepository complaintRepository)
    {
        _complaintRepository = complaintRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComplaintModel>>> GetComplaintsAsync()
    {
        var response = await _complaintRepository.GetAllAsync();
        return Ok(response);
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
