using TestTask.Application.DTOs.Contact;

namespace TestTask.Application.Interfaces;

public interface IContactService
{
    Task<CreateContactResponseDto> CreateContactAsync(CreateContactRequestDto dto, CancellationToken cancellationToken);
}
