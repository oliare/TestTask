namespace TestTask.Domain.Entities;

public record ContactEntity
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public int? AccountId { get; set; }
    public AccountEntity? Account { get; set; }
}
