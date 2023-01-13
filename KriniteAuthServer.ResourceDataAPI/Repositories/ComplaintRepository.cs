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
        return await _complaintDbContext.Complaints
            .Include(applicant => applicant.Applicant)
            .ToListAsync();
    }

    public async Task<ComplaintModel> GetByIdAsync(Guid id)
    {
        return await _complaintDbContext.Complaints
            .Include(applicant => applicant.Applicant)
            .FirstOrDefaultAsync(complaint => complaint.Id == id);
    }

    public async Task<Guid> AddAsync(ComplaintModel complaintModel)
    {
        var addedEntity = await _complaintDbContext.Complaints
            .AddAsync(complaintModel);
        await _complaintDbContext.SaveChangesAsync();

        return addedEntity.Entity.Id;
    }

    public async Task UpdateAsync(ComplaintModel complaintModel)
    {
        _complaintDbContext.Complaints
            .Update(complaintModel);
        await _complaintDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await _complaintDbContext.Complaints
            .Where(complaint => complaint.Id == id)
            .ExecuteDeleteAsync();
    }
}
