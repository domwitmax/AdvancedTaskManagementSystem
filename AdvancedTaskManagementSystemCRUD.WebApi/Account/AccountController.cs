using AdvancedTaskManagementSystemCRUD.Application.Services.Account;
using AdvancedTaskManagementSystemCRUD.Application.Services.Account.DTOs;
using AdvancedTaskManagementSystemCRUD.Domain.Constants;
using AdvancedTaskManagementSystemCRUD.WebApi.Account.Request;
using AdvancedTaskManagementSystemCRUD.WebApi.Account.Response;
using AdvancedTaskManagementSystemCRUD.WebApi.Extensions;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTaskManagementSystemCRUD.WebApi.Account;

[Route("api/[controller]")]
[ApiController]
public class AccountController(
    IAccountService accountService,
    IMapper mapper,
    IValidator<ChangePasswordDTO> changePasswordValidator,
    IValidator<LoginDTO> loginValidator,
    IValidator<RegisterDTO> registerValidator,
    IValidator<UpdateAccountDTO> updateAccountValidator,
    IValidator<ChangeRoleDTO> changeRoleValidator
    ) : ControllerBase
{
    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var loginDTO = mapper.Map<LoginDTO>(request);
        var result = await loginValidator.ValidateAsync(loginDTO);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        string? jwt = await accountService.Login(loginDTO);
        if (jwt is null)
            return BadRequest("Operacja nie powiodła się. Sprawdź poprawność danych i uprawnienia użytkownika.");

        return Ok(new { token = jwt });
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var registerDTO = mapper.Map<RegisterDTO>(request);
        var result = await registerValidator.ValidateAsync(registerDTO);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        string? jwt = await accountService.Register(registerDTO);
        if (jwt is null)
            return BadRequest("Operacja nie powiodła się. Sprawdź poprawność danych i uprawnienia użytkownika.");

        return Ok(new { token = jwt });
    }

    [Authorize(Roles = RoleNames.ProjectManagement)]
    [HttpGet("[action]")]
    public async Task<ActionResult<List<GetAccountResponse>>> GetAccounts()
    {
        var accounts = await accountService.GetAccounts();
        return Ok(mapper.Map<List<GetAccountResponse>>(accounts));
    }

    [Authorize]
    [HttpGet("[action]/{userName}")]
    public async Task<ActionResult<GetAccountResponse>> GetAccount([FromRoute] string userName)
    {
        var userDTO = await accountService.GetAccount(userName);
        if (userDTO == null)
            return NotFound();

        return Ok(mapper.Map<GetAccountResponse>(userDTO));
    }

    [Authorize]
    [HttpGet("[action]")]
    public async Task<ActionResult<GetActualAccountResponse>> GetActualAccount()
    {
        var userName = User.GetLogin();
        if (userName is null)
            return BadRequest("Nie udało się odczytać loginu zalogowanego użytkownika z tokenu JWT.");

        var userActualDTO = await accountService.GetActualAccount(userName);
        if (userActualDTO == null)
            return NotFound();

        return Ok(mapper.Map<GetActualAccountResponse>(userActualDTO));
    }

    [Authorize]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetRoles()
    {
        return Ok(await accountService.GetRoles());
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpPut("[action]")]
    public async Task<IActionResult> ChangeRole([FromBody] ChangeRoleRequest request)
    {
        var changeRoleDTO = mapper.Map<ChangeRoleDTO>(request);
        var result = await changeRoleValidator.ValidateAsync(changeRoleDTO);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        if (!await accountService.ChangeRole(changeRoleDTO))
            return BadRequest("Operacja nie powiodła się. Sprawdź poprawność danych i uprawnienia użytkownika.");

        return Ok();
    }

    [Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var changePasswordDTO = mapper.Map<ChangePasswordDTO>(request);
        var result = await changePasswordValidator.ValidateAsync(changePasswordDTO);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        var userName = User.GetLogin();
        if (userName != changePasswordDTO.Login)
            return Forbid();

        string? jwt = await accountService.ChangePasswordAsync(changePasswordDTO);
        if (jwt is null)
            return BadRequest("Operacja nie powiodła się. Sprawdź poprawność danych i uprawnienia użytkownika.");

        return Ok(new { token = jwt });
    }

    [Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountRequest request)
    {
        var updateUserDTO = mapper.Map<UpdateAccountDTO>(request);
        var result = await updateAccountValidator.ValidateAsync(updateUserDTO);
        if (!result.IsValid)
            return BadRequest(result.Errors);

        var userName = User.GetLogin();
        if (userName != updateUserDTO.Login)
            return Forbid();

        string? jwt = await accountService.UpdateAccount(updateUserDTO);
        if (jwt is null)
            return BadRequest("Operacja nie powiodła się. Sprawdź poprawność danych i uprawnienia użytkownika.");

        return Ok(new { token = jwt });
    }
}
