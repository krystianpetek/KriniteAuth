using static OpenIddict.Abstractions.OpenIddictConstants;

namespace KriniteAuth.OpenIddict.Server;

public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddOpenIddict(openIddictBuilder =>
        {
            openIddictBuilder.AddServer(openIddictServerBuilder =>
            {
                openIddictServerBuilder
                .AllowAuthorizationCodeFlow() // authorization code flow is enabled
                .AllowClientCredentialsFlow() // client credentials flow is enabled
                .AllowRefreshTokenFlow() // refresh token flow is enabled
                .RequireProofKeyForCodeExchange(); // require PKCE for authorization code flow

                openIddictServerBuilder
                .SetAuthorizationEndpointUris("/connect/authorize") // authorization endpoint is exposed at /connect/authorize
                .SetLogoutEndpointUris("/connect/logout") // logout endpoint is exposed at /connect/logout
                .SetTokenEndpointUris("/connect/token") // token endpoint is exposed at /connect/token
                .SetUserinfoEndpointUris("/connect/userinfo") // userinfo endpoint is exposed at /connect/userinfo
                .SetVerificationEndpointUris("/connect/verify"); // verification endpoint is exposed at /connect/verify

                openIddictServerBuilder
                .AddDevelopmentSigningCertificate() // register the development certificate used to sign the JWT tokens
                .AddDevelopmentEncryptionCertificate() // register the development certificate used to encrypt the tokens
                .DisableAccessTokenEncryption(); // disable the access token encryption to make the demo easier to use TODO - for change

                openIddictServerBuilder.RegisterScopes(
                    Scopes.Email, 
                    Scopes.Profile, 
                    Scopes.Roles, 
                    Scopes.OpenId,
                    Scopes.OfflineAccess); // register the scopes supported by the application

                openIddictServerBuilder
                .UseAspNetCore()
                .EnableAuthorizationEndpointPassthrough()
                .EnableLogoutEndpointPassthrough()
                .EnableTokenEndpointPassthrough()
                .EnableUserinfoEndpointPassthrough()
                .EnableVerificationEndpointPassthrough()
                .EnableStatusCodePagesIntegration();
            });
            openIddictBuilder.AddValidation(openIddictValidationBuilder =>
            {
                openIddictValidationBuilder
                .EnableAuthorizationEntryValidation()
                .EnableTokenEntryValidation()
                .UseAspNetCore();
                    //.Configure(openIddictValidationAspNetCoreOptions =>
                    //{
                    //    openIddictValidationAspNetCoreOptions
                    //});
            });
        });

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
