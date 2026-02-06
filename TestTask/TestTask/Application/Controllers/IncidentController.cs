using Microsoft.AspNetCore.Mvc;
using TestTask.Application.DTOs.Incident;
using TestTask.Application.Interfaces;

namespace TestTask.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IncidentController : ControllerBase
{
    private readonly IIncidentService _service;

    public IncidentController(IIncidentService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<CreateIncidentResponseDto>> CreateIncident([FromBody] CreateIncidentRequestDto dto, CancellationToken cancellationToken)
    {
        var result = await _service.CreateIncidentAsync(dto, cancellationToken);
        return result;
    }
}