using OpenIddict.EntityFrameworkCore.Models;

namespace KriniteAuth.OpenIddict.Server.Entities;

public class Authorization : OpenIddictEntityFrameworkCoreAuthorization<int, Application, Token>
{
}
