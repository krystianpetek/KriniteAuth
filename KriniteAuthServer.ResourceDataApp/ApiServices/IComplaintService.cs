using KriniteAuthServer.ResourceDataClient.Models;

namespace KriniteAuthServer.ResourceDataClient.ApiServices;

public interface IComplaintService
{
    Task<IEnumerable<ComplaintModel>> GetAllAsync();
    Task<ComplaintModel> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(ComplaintModel complaintModel);
    Task<int> UpdateAsync(ComplaintModel complaintModel);
    Task<int> DeleteAsync(Guid id);
}
