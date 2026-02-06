using Microsoft.AspNetCore.Mvc;
using TestTask.Application.DTOs.Contact;
using TestTask.Application.Interfaces;

namespace TestTask.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContactController : ControllerBase
{
    private readonly IContactService _service;
    public ContactController(IContactService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<CreateContactResponseDto>> CreateContact([FromBody] CreateContactRequestDto dto, CancellationToken cancellationToken)
    {
        var contact = await _service.CreateContactAsync(dto, cancellationToken);
        return contact;
    }
}
