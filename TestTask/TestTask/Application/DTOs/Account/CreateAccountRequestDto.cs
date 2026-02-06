namespace TestTask.Application.DTOs.Account;

public record CreateAccountRequestDto
{
    public required string Name { get; set; }
    public required int ContactId { get; set; }
}
