using KriniteAuthServer.ResourceDataApp.Models;
using System.Text.Json;
using Microsoft.Net.Http.Headers;

namespace KriniteAuthServer.ResourceDataApp.HttpHandlers;

public class AuthenticationDelegatingHandler : DelegatingHandler
{
    private readonly AuthConfig authConfig;
    private readonly IHttpClientFactory _httpClientFactory;

    public AuthenticationDelegatingHandler(AuthConfig authConfig, IHttpClientFactory httpClientFactory)
    {
        this.authConfig = authConfig;
        _httpClientFactory = httpClientFactory;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient("OAuthServer");

        var token = await httpClient.SendAsync(new HttpRequestMessage
        {
            RequestUri = new Uri(authConfig.RequestUri),
            Method = HttpMethod.Post,
            Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("grant_type", authConfig.GrantType),
                    new KeyValuePair<string, string>("client_id", authConfig.ClientId),
                    new KeyValuePair<string, string>("client_secret", authConfig.ClientSecret),
                    new KeyValuePair<string, string>("scope", authConfig.Scope)
                }),
        });

        var deserializedToken = JsonSerializer.Deserialize<BearerToken>(await token.Content.ReadAsStringAsync()) ?? new BearerToken();
        request.Headers.Add(HeaderNames.Authorization, $"Bearer {deserializedToken.access_token}");

        return await base.SendAsync(request, cancellationToken);
}
}
