using Microsoft.EntityFrameworkCore;
using TestTask.Application.DTOs.Account;
using TestTask.Application.Exceptions;
using TestTask.Application.Interfaces;
using TestTask.Domain.Entities;
using TestTask.Infrastructure.Data;

namespace TestTask.Application.Services;

public class AccountService : IAccountService
{
    private readonly AppDbContext _context;

    public AccountService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CreateAccountResponseDto> CreateAccountAsync(CreateAccountRequestDto dto, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.Id == dto.ContactId, cancellationToken)
            ?? throw new EntityNotFoundException("Contact", dto.ContactId);

        if (contact.AccountId != null)
            throw new Exception("Contact is already linked to an account");

        var accountExists = await _context.Accounts
            .AnyAsync(a => a.Name == dto.Name, cancellationToken);

        if (accountExists)
            throw new Exception($"Account with name '{dto.Name}' already exists");

        var account = new AccountEntity
        {
            Name = dto.Name
        };

        _context.Accounts.Add(account);

        contact.Account = account;

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateAccountResponseDto
        {
            Id = account.Id,
            Name = account.Name
        };
    }
}
