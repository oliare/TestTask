using Microsoft.EntityFrameworkCore;
using TestTask.Application.DTOs.Contact;
using TestTask.Application.Exceptions;
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
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.Email == dto.Email, cancellationToken);

        if (contact != null)
        {
            contact.FirstName = dto.FirstName;
            contact.LastName = dto.LastName;
        }
        else
        {
            contact = new ContactEntity
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email
            };
            await _context.Contacts.AddAsync(contact, cancellationToken);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateContactResponseDto
        {
            Id = contact.Id,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Email = contact.Email
        };
    }

    public async Task<CreateContactResponseDto?> GetContactByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.Email == email, cancellationToken);

        if (contact == null) return null;

        return new CreateContactResponseDto
        {
            Id = contact.Id,
            FirstName = contact.FirstName,
            LastName = contact.LastName,
            Email = contact.Email
        };
    }
}
