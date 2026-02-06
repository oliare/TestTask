using TestTask.Application.DTOs.Account;
using TestTask.Domain.Entities;

namespace TestTask.Application.Interfaces;

public interface IAccountService
{
    Task<CreateAccountResponseDto> CreateAccountAsync(CreateAccountRequestDto dto, CancellationToken cancellationToken);
}
