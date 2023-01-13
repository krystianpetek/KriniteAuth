using KriniteAuthServer.ResourceDataAPI.Models;

namespace KriniteAuthServer.ResourceDataAPI.Repositories;

public interface IComplaintRepository
{
    Task<IEnumerable<ComplaintModel>> GetAllAsync();
    Task<ComplaintModel> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(ComplaintModel complaintModel);
    Task UpdateAsync(ComplaintModel complaintModel);
    Task DeleteAsync(Guid id);
}
