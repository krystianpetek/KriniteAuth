using KriniteAuth.ResourceDataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KriniteAuth.ResourceDataAPI.Data;

public class ComplaintDbContext : DbContext
{
	public ComplaintDbContext(DbContextOptions<ComplaintDbContext> options) : base(options) { }

	public DbSet<ComplaintModel> Complaints { get; set; }
	public DbSet<ApplicantModel> Applicants { get; set; }
}
