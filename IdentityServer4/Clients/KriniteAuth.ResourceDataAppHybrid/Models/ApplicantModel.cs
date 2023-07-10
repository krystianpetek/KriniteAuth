namespace KriniteAuth.ResourceDataAppHybrid.Models;

public record ApplicantModel
{
	public Guid Id { get; set; }

	public string? Name { get; set; }

	public string? Surname { get; set; }
}