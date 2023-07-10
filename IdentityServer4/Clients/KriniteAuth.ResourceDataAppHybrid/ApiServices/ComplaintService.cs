using IdentityModel.Client;
using KriniteAuth.ResourceDataAppHybrid.Models;
using KriniteAuth.ResourceDataAppHybrid.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Security.Claims;

namespace KriniteAuth.ResourceDataAppHybrid.ApiServices;

public class ComplaintService : IComplaintService
{
	private readonly IHttpClientFactory _httpClientFactory;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public ComplaintService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
	{
		_httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
		_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
	}

	public async Task<Guid> AddAsync(ComplaintModel complaintModel)
	{
		HttpClient httpClient = _httpClientFactory.CreateClient("ResourceDataAPI");
		var result = await httpClient.PostAsJsonAsync($"{ApiEndpoints.Complaints}", complaintModel);
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

		var result = await response.Content.ReadFromJsonAsync<IEnumerable<ComplaintModel>>();

		return result ?? Enumerable.Empty<ComplaintModel>();
	}

	public async Task<ComplaintModel> GetByIdAsync(Guid id)
	{
		HttpClient httpClient = _httpClientFactory.CreateClient("ResourceDataAPI");

		var result = await httpClient.GetFromJsonAsync<ComplaintModel>($"{ApiEndpoints.Complaints}/{id}");
		return result;
	}

	public async Task UpdateAsync(ComplaintModel complaintModel)
	{
		HttpClient httpClient = _httpClientFactory.CreateClient("ResourceDataAPI");

		var result = await httpClient.PutAsJsonAsync($"{ApiEndpoints.Complaints}/{complaintModel.Id}", complaintModel);
		var response = await result.Content.ReadAsStringAsync();

		await Task.CompletedTask;
	}

	public async Task<UserInfoViewModel> GetUserInformation()
	{
		var httpClient = _httpClientFactory.CreateClient("OAuthServer");

		var dataResponse = await httpClient.GetDiscoveryDocumentAsync();
		if (dataResponse.IsError)
			throw new HttpRequestException("Error while requesting access token.");

		var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

		var userInfoResponse = await httpClient.GetUserInfoAsync(new UserInfoRequest
		{
			Address = dataResponse.UserInfoEndpoint,
			Token = accessToken
		});
		if (userInfoResponse.IsError)
			throw new HttpRequestException("Error while getting user info.");

		var userInfoClaims = new List<KeyValuePair<string, string>>();
		foreach (Claim claim in userInfoResponse.Claims)
		{
			userInfoClaims.Add(new KeyValuePair<string, string>(claim.Type, claim.Value));
		}

		return new UserInfoViewModel(userInfoClaims);
	}
}
