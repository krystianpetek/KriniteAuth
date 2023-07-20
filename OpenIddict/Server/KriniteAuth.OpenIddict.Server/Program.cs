using KriniteAuth.OpenIddict.Server.Extensions;

namespace KriniteAuth.OpenIddict.Server;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddExtensionOpenIddict();

        var app = builder.Build();
        
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapGet("/", (HttpContext httpContext) =>
        {
            Results.Ok("Hello in KriniteAuth.OpenIddict Server");
        })
        .WithName("Starting point")
        .WithOpenApi();

        app.Run();
    }
}
