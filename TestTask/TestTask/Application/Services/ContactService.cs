using TestTask.Application.DTOs.Contact;
using TestTask.Application.Interfaces;
using TestTask.Domain.Entities;
using TestTask.Infrastructure.Data;

namespace TestTask.Application.Services;

public class ContactService : IContactService
{
    private readonly AppDbContext _context;

    public ContactService(AppDbContext context) => _context = context;

    public async Task<CreateContactResponseDto> CreateContactAsync(CreateContactRequestDto dto, CancellationToken cancellationToken)
    {
        var res = await _context.Contacts.AddAsync(
            new ContactEntity()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
            }, cancellationToken);
        
        await _context.SaveChangesAsync(cancellationToken);

        return new CreateContactResponseDto()
        {
            Id = res.Entity.Id,
            FirstName = res.Entity.FirstName,
            LastName = res.Entity.LastName,
            Email = res.Entity.Email,
        };
    }
}
