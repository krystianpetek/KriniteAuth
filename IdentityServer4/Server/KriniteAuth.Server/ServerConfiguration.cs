using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace KriniteAuth.Server;

public static class ServerConfiguration
{
	internal static List<Client> Clients = new List<Client>()
	{
		new Client
		{
			ClientId = "resourceDataApiClient",
			ClientSecrets =
			{
				new Secret("secret".Sha256())
			},
			AllowedScopes = {"resourceDataApi" },
			AllowedGrantTypes = { GrantType.ClientCredentials }
		},
		new Client
		{
			ClientId = "resourceDataAppClient",
			ClientName = "ResourceDataApp",
			AllowedGrantTypes = { GrantType.AuthorizationCode },
			RequirePkce= true,
			AllowRememberConsent = false,
			RedirectUris = new List<string>
			{
				"https://localhost:7823/signin-oidc"
			},
			PostLogoutRedirectUris = new List<string>
			{
				"https://localhost:7823/signout-callback-oidc"
			},
			ClientSecrets = new List<Secret>
			{
				new Secret("secret".Sha256())
			},
			AllowedScopes = new List<string>
			{
				IdentityServerConstants.StandardScopes.OpenId,
				IdentityServerConstants.StandardScopes.Profile
			}
		},
		new Client
		{
			ClientId = "resourceDataAppClientHybrid",
			ClientName = "ResourceDataAppHybrid",
			AllowedGrantTypes = { GrantType.Hybrid },
			RequirePkce= false,
			AllowRememberConsent = false,
			RedirectUris = new List<string>
			{
				"https://localhost:7824/signin-oidc"
			},
			PostLogoutRedirectUris = new List<string>
			{
				"https://localhost:7824/signout-callback-oidc"
			},
			ClientSecrets = new List<Secret>
			{
				new Secret("secret".Sha256())
			},
			AllowedScopes = new List<string>
			{
				IdentityServerConstants.StandardScopes.OpenId,
				IdentityServerConstants.StandardScopes.Profile,
				IdentityServerConstants.StandardScopes.Email,
				IdentityServerConstants.StandardScopes.Address,
				"resourceDataApi",
				"roles"
			}
		}
	};

	internal static List<ApiScope> ApiScopes = new List<ApiScope>()
	{
		new ApiScope("resourceDataApi","ResourceDataAPI")
	};

	internal static List<ApiResource> ApiResources = new List<ApiResource>()
	{
	};

	internal static List<IdentityResource> IdentityResources = new List<IdentityResource>()
	{
		new IdentityResources.OpenId(),
		new IdentityResources.Profile(),
		new IdentityResources.Email(),
		new IdentityResources.Address(),
		new IdentityResource("roles", "Your roles" ,new List<string>(){ "role" })
	};

	internal static List<TestUser> TestUsers = new List<TestUser>()
	{
		new TestUser
		{
			SubjectId = "9c5880bd-a588-4634-9586-8dda315e5776",
			Username = "krystian",
			Password = "DoNotTellAny0ne",
			Claims = new List<Claim>
			{
				new Claim(JwtClaimTypes.Name, "Krystian Petek"),
				new Claim(JwtClaimTypes.GivenName, "Krystian"),
				new Claim(JwtClaimTypes.FamilyName,"Petek"),
				new Claim(JwtClaimTypes.Email,"krystianpetek2@gmail.com", ClaimValueTypes.Email),
				new Claim(JwtClaimTypes.EmailVerified,"true",ClaimValueTypes.Boolean),
				new Claim(JwtClaimTypes.Address,"Wadowice"),
				new Claim(JwtClaimTypes.Role, "admin")
			},
			IsActive= true
		},        new TestUser
		{
			SubjectId = "9c5880bd-a588-4634-9586-8dda315e7776",
			Username = "localuser",
			Password = "DoNotTellAny0ne",
			Claims = new List<Claim>
			{
				new Claim(JwtClaimTypes.Name, "Local User"),
				new Claim(JwtClaimTypes.GivenName, "local"),
				new Claim(JwtClaimTypes.FamilyName,"user"),
				new Claim(JwtClaimTypes.Email,"localuser@wp.com", ClaimValueTypes.Email),
				new Claim(JwtClaimTypes.EmailVerified,"false",ClaimValueTypes.Boolean),
				new Claim(JwtClaimTypes.Address,"Computer"),
				new Claim(JwtClaimTypes.Role, "user")
			},
			IsActive= true
		}

	};

}
