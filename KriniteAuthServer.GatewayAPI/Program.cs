namespace KriniteAuthServer.GatewayAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");

        app.Run();
    }
}
