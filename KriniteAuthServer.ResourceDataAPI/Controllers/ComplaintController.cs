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
        var result = await _complaintRepository.GetByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddComplaintAsync(ComplaintModel complaintModel)
    {
        var createdId = await _complaintRepository.AddAsync(complaintModel);
        return Created(string.Empty,createdId);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateComplaintAsync(ComplaintModel complaintModel)
    {
        await _complaintRepository.UpdateAsync(complaintModel);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveComplaintAsync(Guid id)
    {
        await _complaintRepository.DeleteAsync(id);
        return Ok();
    }

}
