namespace TestTask.Application.DTOs.Account;

public record CreateAccountResponseDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
