using KriniteAuth.ResourceDataAPI.Data;
using KriniteAuth.ResourceDataAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;

namespace KriniteAuth.ResourceDataAPI;

public static class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		IdentityModelEventSource.ShowPII = true;

		builder.Services.AddControllers();
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen(swagger =>
		{
			swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "ResourceDataAPI", Version = "v1" });
		});

		builder.Services.AddDbContext<ComplaintDbContext>(dbContext =>
		{
			string connectionString = builder.Configuration.GetConnectionString("ComplaintDb");
			dbContext.UseSqlServer(connectionString);
		});

		builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();

		builder.Services.AddAuthentication("Bearer")
			.AddJwtBearer("Bearer", options =>
			{

				options.Authority = "https://localhost:7822";
				options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = false,
				};

				if (builder.Environment.IsEnvironment("Docker"))
				{
					options.Authority = builder.Configuration.GetRequiredSection("Authority").Value;
					options.RequireHttpsMetadata = false;
				}
			});

		builder.Services.AddAuthorization(options =>
		{
			options.AddPolicy("ComplaintPolicy", policy =>
			{
				policy.RequireClaim("client_id", "resourceDataApiClient", "resourceDataAppClientHybrid");
			});
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

		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		await app.SeedData();
		await app.RunAsync();
	}
}
