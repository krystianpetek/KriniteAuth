
using KriniteAuth.OpenIddict.Server.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KriniteAuth.OpenIddict.Server.Persistance.Context;

public class OpenIddictDbContext : IdentityDbContext<User>
{
    public DbSet<Authorization> Authorizations { get; set; }
    public DbSet<Application> Applications { get; set; }
    public DbSet<Scope> Scopes { get; set; }
    public DbSet<Token> Tokens { get; set; }

    public OpenIddictDbContext(DbContextOptions options) : base(options)
    {
    }
}
