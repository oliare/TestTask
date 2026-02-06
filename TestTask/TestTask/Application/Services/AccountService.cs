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
        var contact = await _context.Contacts.FindAsync([dto.ContactId], cancellationToken: cancellationToken) 
            ?? throw new EntityNotFoundException("Contact", dto.ContactId);

        var result = await _context.Accounts.AddAsync(new AccountEntity()
        {
            Name = dto.Name,
            Contacts = [contact]
        }, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateAccountResponseDto()
        {
            Name = result.Entity.Name,
            Id = result.Entity.Id
        };
    }
}
