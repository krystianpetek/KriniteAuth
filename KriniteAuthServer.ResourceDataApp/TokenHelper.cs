using IdentityModel.Client;
using System.Net.Http;
using System.Text.Json;

namespace KriniteAuthServer.ResourceDataApp;

public static class TokenHelper
{
    public static async Task<BearerToken> GetToken(HttpClient _httpClient)
    {
        var token = await _httpClient.SendAsync(new HttpRequestMessage
        {
            RequestUri = new Uri("https://localhost:7822/connect/token"),
            Method = HttpMethod.Post,
            Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", "resourceDataApiClient"),
                new KeyValuePair<string, string>("client_secret", "secret"),
                new KeyValuePair<string, string>("scope", "resourceDataApi")
            }),
        });
        
        var deserializedToken = JsonSerializer.Deserialize<BearerToken>(await token.Content.ReadAsStringAsync()) ?? new BearerToken();
        _httpClient.SetBearerToken(deserializedToken.access_token);
        
        return deserializedToken;
    }

    public class BearerToken
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
    }
}
