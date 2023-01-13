using IdentityServer4.Models;
using IdentityServer4.Test;

namespace KriniteAuthServer.OAuthServer;

public static class ServerConfiguration
{
    public static List<Client> Clients = new List<Client>()
    {
    };

    public static List<ApiScope> ApiScopes = new List<ApiScope>()
    {
    };

    public static List<ApiResource> ApiResources = new List<ApiResource>()
    {
    };

    public static List<IdentityResource> IdentityResources = new List<IdentityResource>()
    {
    };

    public static List<TestUser> TestUsers = new List<TestUser>()
    {
    };

}
