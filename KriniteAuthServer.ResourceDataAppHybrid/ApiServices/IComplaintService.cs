using KriniteAuthServer.ResourceDataAppHybrid.Models;

namespace KriniteAuthServer.ResourceDataAppHybrid.ApiServices;

public interface IComplaintService
{
    Task<IEnumerable<ComplaintModel>> GetAllAsync();
    Task<ComplaintModel> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(ComplaintModel complaintModel);
    Task UpdateAsync(ComplaintModel complaintModel);
    Task<int> DeleteAsync(Guid id);
}
