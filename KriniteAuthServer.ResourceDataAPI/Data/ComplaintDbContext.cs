using KriniteAuthServer.ResourceDataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KriniteAuthServer.ResourceDataAPI.Data;

public class ComplaintDbContext : DbContext
{
    public ComplaintDbContext(DbContextOptions<ComplaintDbContext> options) : base(options) { }

    public DbSet<ComplaintModel> Complaints { get; set; }
}
