using KriniteAuth.ResourceDataAPI.Models;
using KriniteAuth.ResourceDataAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KriniteAuth.ResourceDataAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "ComplaintPolicy")]
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
		if (result is null)
			return NotFound();
		return Ok(result);
	}

	[HttpPost]
	public async Task<IActionResult> AddComplaintAsync(ComplaintModel complaintModel)
	{
		var createdId = await _complaintRepository.AddAsync(complaintModel);
		return Created(string.Empty, createdId);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> UpdateComplaintAsync(ComplaintModel complaintModel)
	{
		var result = await _complaintRepository.UpdateAsync(complaintModel);
		if (result <= 0)
			return NotFound();

		return Ok(complaintModel);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> RemoveComplaintAsync(Guid id)
	{
		var result = await _complaintRepository.DeleteAsync(id);
		if (result <= 0)
			return NotFound();
		return Ok(result);
	}

}
