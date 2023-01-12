using KriniteAuthServer.ResourceDataAPI.Models;

namespace KriniteAuthServer.ResourceDataAPI.Repositories;

public interface IComplaintRepository
{
    Task<IEnumerable<ComplaintModel>> GetAllAsync();
}
