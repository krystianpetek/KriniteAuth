using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

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
            AllowedGrantTypes = { GrantType.ClientCredentials }
        },
        new Client
        {
            ClientId = "resourceDataAppClient",
            ClientName = "ResourceDataApp",
            AllowedGrantTypes = { GrantType.AuthorizationCode },
            AllowRememberConsent = false,
            RedirectUris = new List<string>
            {
                "http://localhost:7822/signin-oidc"
            },
            PostLogoutRedirectUris = new List<string>
            {
                "https://localhost:7822/signout-callback-oidc"
            },
            ClientSecrets = new List<Secret>
            {
                new Secret("secret".Sha256())
            },
            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
            }

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
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    };

    internal static List<TestUser> TestUsers = new List<TestUser>()
    {
        new TestUser
        {
            SubjectId = "9c5880bd-a588-4634-9586-8dda315e5776",
            Username = "krystian",
            Password = "DoNotTellAny0ne",
            Claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.GivenName, "Krystian"),
                new Claim(JwtClaimTypes.FamilyName,"Petek")
            },
            IsActive= true,
            ProviderName = "KriniteAuthServer.OAuthServer"
        }
    };

}
