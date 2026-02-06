using System.ComponentModel.DataAnnotations;

namespace TestTask.Domain.Entities;

public record IncidentEntity
{
    [Key]
    public string IncidentName { get; set; } = $"INC-{Guid.NewGuid():N}";
    public required string Description { get; set; }
    public ICollection<AccountEntity> Accounts { get; set; } = [];
}
