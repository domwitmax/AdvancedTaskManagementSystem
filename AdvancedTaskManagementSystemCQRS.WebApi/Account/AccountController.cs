using AdvancedTaskManagementSystemCQRS.Application.Common.Account.GetRoles;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.GetActualAccount;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.GetAccount;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.ChangePassword;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.ChangeRole;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.GetAccounts;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.Login;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.Register;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.UpdateAccount;
using AdvancedTaskManagementSystemCQRS.Domain.Constants;
using AdvancedTaskManagementSystemCQRS.WebApi.Account.Request;
using AdvancedTaskManagementSystemCQRS.WebApi.Account.Response;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTaskManagementSystemCQRS.WebApi.Account;

[Route("api/[controller]")]
[ApiController]
public class AccountController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        string? jwt = await mediator.Send(mapper.Map<LoginCommand>(request));
        if (jwt is null)
            return BadRequest();

        return Ok(new { token = jwt });
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        string? jwt = await mediator.Send(mapper.Map<RegisterCommand>(request));
        if (jwt is null)
            return BadRequest();

        return Ok(new { token = jwt });
    }

    [Authorize(Roles = RoleNames.ProjectManagement)]
    [HttpGet("[action]")]
    public async Task<ActionResult<List<GetAccountResponse>>> GetAccounts()
    {
        var accounts = await mediator.Send(new GetAccountsQuery());
        return Ok(mapper.Map<List<GetAccountResponse>>(accounts));
    }

    [Authorize]
    [HttpGet("[action]/{userName}")]
    public async Task<ActionResult<GetAccountResponse>> GetAccount([FromRoute] string userName)
    {
        var userDTO = await mediator.Send(new GetAccountCommonQuery(userName));
        if (userDTO == null)
            return NotFound();

        return Ok(mapper.Map<GetAccountResponse>(userDTO));
    }

    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<GetActualAccountResponse>> GetActualAccount()
    {
        var userName = User.Identity?.Name;
        if (userName is null)
            return BadRequest();

        var userActualDTO = await mediator.Send(new GetActualAccountCommonQuery(userName));
        if (userActualDTO == null)
            return NotFound();

        return Ok(mapper.Map<GetActualAccountResponse>(userActualDTO));
    }

    [Authorize]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetRoles()
    {
        return Ok(await mediator.Send(new GetRolesQuery()));
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeRole([FromBody] ChangeRoleRequest request)
    {
        var result = await mediator.Send(mapper.Map<ChangeRoleCommand>(request));
        if (!result)
            return BadRequest();

        return Ok();
    }

    [Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var userName = User.Identity?.Name;
        if (userName != request.Login)
            return BadRequest();

        string? jwt = await mediator.Send(mapper.Map<ChangePasswordCommand>(request));
        if (jwt is null)
            return BadRequest();

        return Ok(new { token = jwt });
    }

    [Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountRequest request)
    {
        var userName = User.Identity?.Name;
        if (userName != request.Login)
            return BadRequest();

        string? jwt = await mediator.Send(mapper.Map<UpdateAccountCommonCommand>(request));
        if (jwt is null)
            return BadRequest();

        return Ok(new { token = jwt });
    }
}
