
using KriniteAuthServer.ResourceDataAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace KriniteAuthServer.ResourceDataAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen( swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "ResourceDataAPI", Version = "v1" });
        });
        
        builder.Services.AddDbContext<ComplaintDbContext>(dbContext =>
        {
            string connectionString = builder.Configuration.GetConnectionString("ComplaintDb");
            dbContext.UseSqlServer(connectionString);
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Docker"))
        {
            app.UseSwagger();
            app.UseSwaggerUI(swagger =>
            {
                swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "ResourceDataAPI v1");
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
