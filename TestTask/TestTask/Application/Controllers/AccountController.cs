using Microsoft.AspNetCore.Mvc;
using TestTask.Application.DTOs.Account;
using TestTask.Application.Interfaces;

namespace TestTask.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountController(IAccountService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<CreateAccountResponseDto>> CreateAccount([FromBody] CreateAccountRequestDto dto, CancellationToken cancellationToken)
    {
        var account = await _service.CreateAccountAsync(dto, cancellationToken);
        return account;
    }
}