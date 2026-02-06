using TestTask.Application.DTOs.Incident;

namespace TestTask.Application.Interfaces;

public interface IIncidentService
{
    Task<CreateIncidentResponseDto> CreateIncidentAsync(CreateIncidentRequestDto dto, CancellationToken cancellationToken);
}
