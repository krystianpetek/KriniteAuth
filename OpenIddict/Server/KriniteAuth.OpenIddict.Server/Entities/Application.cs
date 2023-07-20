using OpenIddict.EntityFrameworkCore.Models;

namespace KriniteAuth.OpenIddict.Server.Entities;

public class Application : OpenIddictEntityFrameworkCoreApplication<int, Authorization, Token>
{
}
