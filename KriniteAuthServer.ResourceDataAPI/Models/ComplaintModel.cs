namespace KriniteAuthServer.ResourceDataAPI.Models;

public record ComplaintModel
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public PriorityEnum? Priority { get; set; }

    public StatusEnum? State { get; set; }

    public DateTime? Created { get; set; }

    public ApplicantModel Applicant { get; set; }
}