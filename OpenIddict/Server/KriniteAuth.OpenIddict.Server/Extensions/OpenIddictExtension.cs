using KriniteAuth.OpenIddict.Server.Entities;
using KriniteAuth.OpenIddict.Server.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using static OpenIddict.Abstractions.OpenIddictConstants;


namespace KriniteAuth.OpenIddict.Server.Extensions;

public static class OpenIddictExtension
{
    public static IServiceCollection AddExtensionOpenIddict(this IServiceCollection services)
    {
        services.AddOpenIddict(openIddictBuilder =>
        {
            services.AddDbContext<OpenIddictDbContext>((sp, options) =>
            {
                options.UseSqlServer("Data Source=localhost;Initial Catalog=KriniteAuth.OpenIddict;Connection Timeout=15;Integrated Security=True;TrustServerCertificate=True");

                options.UseOpenIddict<Application, Authorization, Scope, Token, int>();
            });

            openIddictBuilder.AddCore(openIddictCoreBuilder =>
            {
                openIddictCoreBuilder
                .UseEntityFrameworkCore()
                .UseDbContext<OpenIddictDbContext>()
                .ReplaceDefaultEntities<Guid>();
            });

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
                .SetAccessTokenLifetime(TimeSpan.FromMinutes(30))
                .SetRefreshTokenLifetime(TimeSpan.FromMinutes(60))
                .SetIdentityTokenLifetime(TimeSpan.FromMinutes(30))
                .SetAuthorizationCodeLifetime(TimeSpan.FromMinutes(5))
                .SetUserCodeLifetime(TimeSpan.FromMinutes(5));

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

        return services;

            //.AddCore(options =>
            //{
            //    options.UseEntityFrameworkCore()
            //        .UseDbContext<OpenIddictDbContext>()
            //        .ReplaceDefaultEntities<Guid>();
            //})
            //.AddServer(options =>
            //{
            //        .AllowAuthorizationCodeFlow()
            //        .AllowRefreshTokenFlow()
            //        .AllowPasswordFlow()
            //        .AllowClientCredentialsFlow()
            //        .AllowImplicitFlow()
            //        .DisableSlidingExpiration()
            
            //        .EnableAuthorizationRequestCaching()
            //        .EnableRequestCaching()
            //        .EnableScopeValidation()
            //        .EnableDegradedMode()
            //        .AddDeferredTokenRevocation()
            //        .AddDeferredKeySetValidation();
            //})
            //.AddValidation(options =>
            //{
            //    options.UseLocalServer();
            //    options.UseAspNetCore();
            //});

        return services;
    }
}
