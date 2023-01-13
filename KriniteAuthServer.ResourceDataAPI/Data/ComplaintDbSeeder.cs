using KriniteAuthServer.ResourceDataAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KriniteAuthServer.ResourceDataAPI.Data;

public static class ComplaintDbSeeder
{
    public async static Task<IApplicationBuilder> SeedData(this IApplicationBuilder app)
    {
        var serviceProvider = app.ApplicationServices.CreateScope().ServiceProvider;
        var logger = serviceProvider.GetRequiredService<ILogger<ComplaintDbContext>>();
        var dbContext = serviceProvider.GetRequiredService<ComplaintDbContext>();

        try
        {
            logger.LogInformation("Seeding database is started.");
            if (dbContext.Database.GetPendingMigrations().Any() || !dbContext.Complaints.Any())
            {
                await dbContext.Database.MigrateAsync();
                await dbContext.AddRangeAsync(ContextData());
                await dbContext.SaveChangesAsync();
            }
            logger.LogInformation("Seeding database is completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured while database are seeding.");
        }

        return app;
    }
    private static IReadOnlyCollection<ComplaintModel> ContextData()
    {
        return new ComplaintModel[]
        {
            new ComplaintModel
            {
                Id = Guid.Parse("6da1229a-3e2b-4bcd-1759-08daf5224668"),
                Title = "Title1",
                Description= "Description1",
                Priority = Priority.LOW,
                Status = Status.SUBMITTED,
                Created= DateTime.Now,
                Applicant = new ApplicantModel
                {
                    Name = "Name1",
                    Surname = "Surname1"
                }
            },
            new ComplaintModel
            {
                Title = "Title2",
                Description= "Description2",
                Priority = Priority.MEDIUM,
                Status = Status.CANCELED,
                Created= DateTime.Now,
                Applicant = new ApplicantModel
                {
                    Name = "Name2",
                    Surname = "Surname2"
                }
            },
            new ComplaintModel
            {
                Title = "Title3",
                Description= "Description3",
                Priority = Priority.HIGH,
                Status = Status.ACCEPTED,
                Created= DateTime.Now,
                Applicant = new ApplicantModel
                {
                    Name = "Name3",
                    Surname = "Surname3"
                }
            },

        };
    }
}
