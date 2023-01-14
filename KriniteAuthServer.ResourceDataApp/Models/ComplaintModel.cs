namespace KriniteAuthServer.ResourceDataClient.Models;

public class ComplaintModel
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public Priority? Priority { get; set; }

    public Status? Status { get; set; }

    public DateTime? Created { get; set; }

    public ApplicantModel? Applicant { get; set; }
}
