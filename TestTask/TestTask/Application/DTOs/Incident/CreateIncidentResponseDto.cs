namespace TestTask.Application.DTOs.Incident;

public record CreateIncidentResponseDto
{
    public required string IncidentName { get; set; }
    public required string IncidentDescription { get; set; }
    public required string AccountName { get; set; }
    public required string ContactEmail { get; set; }
}
