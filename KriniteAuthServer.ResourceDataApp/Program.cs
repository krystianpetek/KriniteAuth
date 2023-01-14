using KriniteAuthServer.ResourceDataClient.ApiServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Logging;

namespace KriniteAuthServer.ResourceDataClient;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IdentityModelEventSource.ShowPII = true;

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<IComplaintService, ComplaintService>();

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "https://localhost:7822";

                options.ClientId = "resourceDataAppClient";
                options.ClientSecret = "secret";
                options.ResponseType = "code";

                options.Scope.Add("openid");
                options.Scope.Add("profile");

                options.SaveTokens = true;

                options.GetClaimsFromUserInfoEndpoint = true;

                if (builder.Environment.IsEnvironment("Docker"))
                {
                    string internalAuthority = builder.Configuration.GetRequiredSection("InternalAuthority").Value;
                    string authority = builder.Configuration.GetRequiredSection("Authority").Value;
                    string redirectUri = builder.Configuration.GetRequiredSection("RedirectUri").Value;

                    options.Authority = internalAuthority;
                    options.Events.OnRedirectToIdentityProvider = (async context =>
                    {
                        context.ProtocolMessage.RedirectUri = string.Join("", redirectUri, "/signin-oidc");
                        context.ProtocolMessage.IssuerAddress = string.Join("",authority, "/connect/authorize");
                        await Task.CompletedTask;
                    });
                    options.RequireHttpsMetadata = false;
                }
            });

        builder.Services.AddHttpClient<ComplaintService>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
