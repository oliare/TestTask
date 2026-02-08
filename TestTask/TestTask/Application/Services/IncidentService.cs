using Microsoft.EntityFrameworkCore;
using TestTask.Application.DTOs.Incident;
using TestTask.Application.Exceptions;
using TestTask.Application.Interfaces;
using TestTask.Domain.Entities;
using TestTask.Infrastructure.Data;

namespace TestTask.Application.Services;

public class IncidentService : IIncidentService
{
    private readonly AppDbContext _context;

    public IncidentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CreateIncidentResponseDto> CreateIncidentAsync(CreateIncidentRequestDto dto, CancellationToken cancellationToken)
    {
        var account = await _context.Accounts
            .FirstOrDefaultAsync(a => a.Name == dto.AccountName, cancellationToken)
            ?? throw new EntityNotFoundException("Account", dto.AccountName);

        var contact = await _context.Contacts
            .FirstOrDefaultAsync(c => c.Email == dto.ContactEmail, cancellationToken);

        if (contact != null)
        {
            contact.FirstName = dto.ContactFirstName;
            contact.LastName = dto.ContactLastName;

            if (contact.AccountId == null) 
                contact.AccountId = account.Id;
        }
        else
        {
            contact = new ContactEntity
            {
                FirstName = dto.ContactFirstName,
                LastName = dto.ContactLastName,
                Email = dto.ContactEmail,
                AccountId = account.Id
            };

            _context.Contacts.Add(contact);
        }

        var incident = new IncidentEntity
        {
            Description = dto.IncidentDescription,
            AccountId = account.Id
        };

        _context.Incidents.Add(incident);

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateIncidentResponseDto
        {
            IncidentName = incident.IncidentName,
            IncidentDescription = incident.Description,
            AccountName = account.Name,
            ContactEmail = contact.Email
        };
    }
}
