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
            .AddInMemoryIdentityResources(ServerConfiguration.IdentityResources)
            .AddInMemoryApiResources(ServerConfiguration.ApiResources)
            .AddInMemoryApiScopes(ServerConfiguration.ApiScopes)
            .AddTestUsers(ServerConfiguration.TestUsers)
             .AddDeveloperSigningCredential();

        var app = builder.Build();

        app.UseIdentityServer();

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}
