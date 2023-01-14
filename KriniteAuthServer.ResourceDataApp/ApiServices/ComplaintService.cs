using IdentityModel.Client;
using KriniteAuthServer.ResourceDataApp;
using KriniteAuthServer.ResourceDataApp.ApiServices;
using KriniteAuthServer.ResourceDataClient.Models;

namespace KriniteAuthServer.ResourceDataClient.ApiServices;

public class ComplaintService : IComplaintService
{
    private readonly HttpClient _httpClient;

    public ComplaintService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Guid> AddAsync(ComplaintModel complaintModel)
    {
        _ = await TokenHelper.GetToken(_httpClient);
        var result = await _httpClient.PostAsJsonAsync<ComplaintModel>($"{ComplaintApiEndpoints.Complaints}",complaintModel);
        var response = await result.Content.ReadFromJsonAsync<Guid>();
        return response;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        _ = await TokenHelper.GetToken(_httpClient);
        var result = await _httpClient.DeleteFromJsonAsync<int>($"{ComplaintApiEndpoints.Complaints}/{id}");
        return result;
    }

    public async Task<IEnumerable<ComplaintModel>> GetAllAsync()
    {
        _ = await TokenHelper.GetToken(_httpClient);

        var result = await _httpClient.GetFromJsonAsync<IEnumerable<ComplaintModel>>(ComplaintApiEndpoints.Complaints);
        return result ?? Enumerable.Empty<ComplaintModel>();
    }

    public async Task<ComplaintModel> GetByIdAsync(Guid id)
    {
        _ = await TokenHelper.GetToken(_httpClient);

        var result = await _httpClient.GetFromJsonAsync<ComplaintModel>($"{ComplaintApiEndpoints.Complaints}/{id}");
        return result;
    }

    public async Task<int> UpdateAsync(ComplaintModel complaintModel)
    {
        _ = await TokenHelper.GetToken(_httpClient);

        var result = await _httpClient.PutAsJsonAsync<ComplaintModel>($"{ComplaintApiEndpoints.Complaints}/{complaintModel.Id}", complaintModel);
        var response = await result.Content.ReadAsStringAsync();

        return await Task.FromResult(1);
    }
}
