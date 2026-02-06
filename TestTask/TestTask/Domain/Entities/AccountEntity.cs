namespace TestTask.Domain.Entities;

public record AccountEntity
{
    public int Id { get; set; }
    public required string Name  { get; set; }

    public IncidentEntity? Incident { get; set; }
    public ICollection<ContactEntity> Contacts { get; set; } = [];
}
