using IdentityModel.Client;
using KriniteAuthServer.ResourceDataApp.ApiServices;
using KriniteAuthServer.ResourceDataClient.Models;
using System.Text.Json;

namespace KriniteAuthServer.ResourceDataClient.ApiServices;

public class ComplaintService : IComplaintService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ComplaintService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Guid> AddAsync(ComplaintModel complaintModel)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient("ResourceDataAPI");
        var result = await httpClient.PostAsJsonAsync<ComplaintModel>($"{ApiEndpoints.Complaints}", complaintModel);
        var response = await result.Content.ReadFromJsonAsync<Guid>();
        return response;
    }

    public async Task<int> DeleteAsync(Guid id)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient("ResourceDataAPI");
        var result = await httpClient.DeleteFromJsonAsync<int>($"{ApiEndpoints.Complaints}/{id}");
        return result;
    }

    public async Task<IEnumerable<ComplaintModel>> GetAllAsync()
    {
        HttpClient httpClient = _httpClientFactory.CreateClient("ResourceDataAPI");
        
        var response = await httpClient.SendAsync(new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri($"{ApiEndpoints.Complaints}")
        }, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync< IEnumerable<ComplaintModel>>();

        return result ?? Enumerable.Empty<ComplaintModel>();
    }

    public async Task<ComplaintModel> GetByIdAsync(Guid id)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient("ResourceDataAPI");

        var result = await httpClient.GetFromJsonAsync<ComplaintModel>($"{ApiEndpoints.Complaints}/{id}");
        return result;
    }

    public async Task<int> UpdateAsync(ComplaintModel complaintModel)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient("ResourceDataAPI");

        var result = await httpClient.PutAsJsonAsync<ComplaintModel>($"{ApiEndpoints.Complaints}/{complaintModel.Id}", complaintModel);
        var response = await result.Content.ReadAsStringAsync();

        return await Task.FromResult(1);
    }
}
