using KriniteAuthServer.ResourceDataAPI.Models;

namespace KriniteAuthServer.ResourceDataAPI.Repositories;

public interface IComplaintRepository
{
    Task<IEnumerable<ComplaintModel>> GetAllAsync();
    Task<ComplaintModel> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(ComplaintModel complaintModel);
    Task<int> UpdateAsync(ComplaintModel complaintModel);
    Task<int> DeleteAsync(Guid id);
}
