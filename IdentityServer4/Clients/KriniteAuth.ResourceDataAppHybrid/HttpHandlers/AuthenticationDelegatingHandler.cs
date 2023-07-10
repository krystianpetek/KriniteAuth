using IdentityModel.Client;
using KriniteAuth.ResourceDataAppHybrid.Models.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace KriniteAuth.ResourceDataAppHybrid.HttpHandlers;

public class AuthenticationDelegatingHandler : DelegatingHandler
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public AuthenticationDelegatingHandler(AuthConfig authConfig, IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
	}

	protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		var accessToken = await _httpContextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
		if (!string.IsNullOrWhiteSpace(accessToken))
			request.SetBearerToken(accessToken);

		return await base.SendAsync(request, cancellationToken);
	}
}
