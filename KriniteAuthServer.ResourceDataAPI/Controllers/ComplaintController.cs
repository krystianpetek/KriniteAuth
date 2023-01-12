using KriniteAuthServer.ResourceDataAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KriniteAuthServer.ResourceDataAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplaintController
{
    public ComplaintController() { }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ComplaintModel>>> GetComplaintsAsync()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ComplaintModel>> GetComplaintAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> AddComplaintAsync()
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ComplaintModel>> UpdateComplaintAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ComplaintModel>> RemoveComplaintAsync(Guid id)
    {
        throw new NotImplementedException();
    }

}
