using KriniteAuthServer.ResourceDataAPI.Data;
using KriniteAuthServer.ResourceDataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KriniteAuthServer.ResourceDataAPI.Repositories;

public class ComplaintRepository : IComplaintRepository
{
    private readonly ComplaintDbContext _complaintDbContext;

    public ComplaintRepository(ComplaintDbContext complaintDbContext)
    {
        _complaintDbContext = complaintDbContext;
    }
    public async Task<IEnumerable<ComplaintModel>> GetAllAsync()
    {
        return await _complaintDbContext.Complaints.ToListAsync();
    }
}
