using OpenIddict.EntityFrameworkCore.Models;

namespace KriniteAuth.OpenIddict.Server.Entities;

public class Token : OpenIddictEntityFrameworkCoreToken<int, Application, Authorization> { }
