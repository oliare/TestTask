namespace TestTask.Domain.Entities;

public record AccountEntity
{
    public int Id { get; set; }
    public required string Name  { get; set; }

    public ICollection<IncidentEntity> Incidents { get; set; } = [];
    public ICollection<ContactEntity> Contacts { get; set; } = [];
}
    