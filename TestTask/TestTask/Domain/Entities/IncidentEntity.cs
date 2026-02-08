using System.ComponentModel.DataAnnotations;

namespace TestTask.Domain.Entities;

public record IncidentEntity
{
    [Key]
    public string IncidentName { get; set; } = $"INC-{Guid.NewGuid():N}";
    public required string Description { get; set; }
    public int AccountId { get; set; }
    public AccountEntity Account { get; set; }
}
