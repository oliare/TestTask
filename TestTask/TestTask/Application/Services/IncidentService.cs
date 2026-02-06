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
            .Include(a => a.Contacts)
            .Include(a => a.Incident)
            .FirstOrDefaultAsync(a => a.Name == dto.AccountName, cancellationToken)
            ?? throw new EntityNotFoundException("Account", dto.AccountName);

        var contact = await _context.Contacts
            .Include(x => x.Account)
            .FirstOrDefaultAsync(c => c.Email == dto.ContactEmail, cancellationToken);

        if (contact is not null)
        {
            contact.FirstName = dto.ContactFirstName;
            contact.LastName = dto.ContactLastName;
            contact.Email = dto.ContactEmail;
            contact.Account ??= account;
        }
        else
        {
            var contactResult = await _context.Contacts.AddAsync(new ContactEntity
            {
                FirstName = dto.ContactFirstName,
                LastName = dto.ContactLastName,
                Email = dto.ContactEmail
            }, cancellationToken);

            contact = contactResult.Entity;
            account.Contacts.Add(contact);

        }

        var incidentResult = await _context.Incidents.AddAsync(new IncidentEntity()
        {
            Description = dto.IncidentDescription
        });

        account.Incident = incidentResult.Entity;

        await _context.SaveChangesAsync(cancellationToken);

        return new CreateIncidentResponseDto()
        {
            IncidentName = incidentResult.Entity.IncidentName,
            IncidentDescription = incidentResult.Entity.Description,
            AccountName = account.Name,
            ContactEmail = contact.Email
        };
    }
}
