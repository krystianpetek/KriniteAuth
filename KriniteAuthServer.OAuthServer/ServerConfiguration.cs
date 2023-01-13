using IdentityServer4.Models;
using IdentityServer4.Test;

namespace KriniteAuthServer.OAuthServer;

public static class ServerConfiguration
{
    internal static List<Client> Clients = new List<Client>()
    {
        new Client
        {
            ClientId = "resourceDataApiClient",
            ClientSecrets =
            {
                new Secret("secret".Sha256())
            },
            AllowedScopes = {"resourceDataApi" },
            AllowedGrantTypes = {GrantType.ClientCredentials }
        }
    };

    internal static List<ApiScope> ApiScopes = new List<ApiScope>()
    {
        new ApiScope("resourceDataApi","ResourceDataAPI")
    };

    internal static List<ApiResource> ApiResources = new List<ApiResource>()
    {
    };

    internal static List<IdentityResource> IdentityResources = new List<IdentityResource>()
    {
    };

    internal static List<TestUser> TestUsers = new List<TestUser>()
    {
    };

}
