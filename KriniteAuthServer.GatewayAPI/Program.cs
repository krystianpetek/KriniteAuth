using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace KriniteAuthServer.GatewayAPI;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);
        builder.Services.AddOcelot();

        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");

        await app.UseOcelot();
        app.Run();
    }
}
