namespace TestTask.Application.DTOs.Incident;

public record CreateIncidentRequestDto
{
    public required string AccountName { get; set; }
    public required string ContactFirstName { get; set; }
    public required string ContactLastName { get; set; }
    public required string ContactEmail { get; set; }
    public required string IncidentDescription { get; set; }
}
