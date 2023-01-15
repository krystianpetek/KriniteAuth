using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Logging;
using Microsoft.Net.Http.Headers;
using KriniteAuthServer.ResourceDataAppHybrid.Models;
using KriniteAuthServer.ResourceDataAppHybrid.ApiServices;
using KriniteAuthServer.ResourceDataAppHybrid.HttpHandlers;

namespace KriniteAuthServer.ResourceDataAppHybrid;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        IdentityModelEventSource.ShowPII = true;

        builder.Services.AddControllersWithViews();
        builder.Services.AddScoped<IComplaintService, ComplaintService>();
        builder.Services.AddSingleton(builder.Configuration.GetRequiredSection("AuthConfig").Get<AuthConfig>());

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "https://localhost:7822";

                options.ClientId = "resourceDataAppClientHybrid";
                options.ClientSecret = "secret";
                options.ResponseType = "code id_token";

                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("resourceDataApi");

                options.SaveTokens = true;

                options.GetClaimsFromUserInfoEndpoint = true;

                if (builder.Environment.IsEnvironment("Docker"))
                {
                    string internalAuthority = builder.Configuration.GetRequiredSection("InternalAuthority").Value;
                    string authority = builder.Configuration.GetRequiredSection("Authority").Value;
                    string redirectUri = builder.Configuration.GetRequiredSection("RedirectUri").Value;

                    options.Authority = internalAuthority;
                    options.Events.OnRedirectToIdentityProvider = async context =>
                    {
                        context.ProtocolMessage.RedirectUri = string.Join("", redirectUri, "/signin-oidc");
                        context.ProtocolMessage.IssuerAddress = string.Join("", authority, "/connect/authorize");
                        await Task.CompletedTask;
                    };

                    options.Events.OnRedirectToIdentityProviderForSignOut = async context =>
                    {
                        string authority = builder.Configuration.GetRequiredSection("Authority").Value;
                        string redirectUri = builder.Configuration.GetRequiredSection("RedirectUri").Value;

                        context.ProtocolMessage.IssuerAddress = string.Join("", authority, "/connect/endsession");
                        context.ProtocolMessage.PostLogoutRedirectUri = string.Join("", redirectUri, "/signout-callback-oidc");
                        await Task.CompletedTask;
                    };
                    options.RequireHttpsMetadata = false;
                }
            });

        builder.Services.AddHttpClient("OAuthServer", async httpClient =>
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        });

        builder.Services.AddTransient<AuthenticationDelegatingHandler>();
        builder.Services.AddHttpClient("ResourceDataAPI", httpClient =>
        {
            httpClient.BaseAddress = new Uri(ApiEndpoints.Complaints);
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
        }).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

        builder.Services.AddHttpContextAccessor();

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
