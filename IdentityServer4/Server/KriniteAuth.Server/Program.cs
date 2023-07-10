namespace KriniteAuth.Server;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);
		builder.Services.AddControllersWithViews();

		builder.Services.AddIdentityServer(options =>
			{
				if (builder.Environment.IsEnvironment("Docker"))
					options.IssuerUri = builder.Configuration.GetRequiredSection("Issuer").Value;
			})
			.AddInMemoryClients(ServerConfiguration.Clients)
			.AddInMemoryApiScopes(ServerConfiguration.ApiScopes)
			.AddInMemoryApiResources(ServerConfiguration.ApiResources)
			.AddInMemoryIdentityResources(ServerConfiguration.IdentityResources)
			.AddTestUsers(ServerConfiguration.TestUsers)
			.AddDeveloperSigningCredential();

		var app = builder.Build();

		app.UseIdentityServer();

		app.UseStaticFiles();
		app.UseAuthorization();
		app.MapDefaultControllerRoute();

		app.Run();
	}
}
