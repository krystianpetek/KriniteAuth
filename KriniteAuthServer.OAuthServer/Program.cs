using IdentityServer4.Models;
using IdentityServer4.Test;

namespace KriniteAuthServer.OAuthServer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddIdentityServer()
            .AddInMemoryClients(ServerConfiguration.Clients)
            .AddInMemoryApiScopes(ServerConfiguration.ApiScopes)
            .AddInMemoryApiResources(ServerConfiguration.ApiResources)
            .AddInMemoryIdentityResources(ServerConfiguration.IdentityResources)
            .AddTestUsers(ServerConfiguration.TestUsers)
            .AddDeveloperSigningCredential();

        var app = builder.Build();

        app.UseIdentityServer();

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}
