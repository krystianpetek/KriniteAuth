using KriniteAuthServer.ResourceDataClient.Models;

namespace KriniteAuthServer.ResourceDataClient.ApiServices;

public class ComplaintService : IComplaintService
{
    private readonly HttpClient _httpClient;

    public Task<Guid> AddAsync(ComplaintModel complaintModel)
    {
        throw new NotImplementedException();
    }

    public Task<int> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ComplaintModel>> GetAllAsync()
    {
        return await Task.FromResult(new ComplaintModel[]
        {
            new ComplaintModel
            {
                Id = Guid.Parse("6da1229a-3e2b-4bcd-1759-08daf5224668"),
                Title = "Title1",
                Description= "Description1",
                Priority = Priority.LOW,
                Status = Status.SUBMITTED,
                Created= DateTime.Now,
                Applicant = new ApplicantModel
                {
                    Name = "Name1",
                    Surname = "Surname1"
                }
            },
        });
    }

    public Task<ComplaintModel> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(ComplaintModel complaintModel)
    {
        throw new NotImplementedException();
    }
}
