using AdvancedTaskManagementSystemCRUD.Application.Services.Account.DTOs;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Account;

public interface IAccountService
{
    Task<string?> Login(LoginDTO loginDTO);
    Task<string?> Register(RegisterDTO loginDTO);
    Task<GetAccountDTO?> GetAccount(string userName);
    Task<List<GetAccountDTO>> GetAccounts();
    Task<List<RoleDTO>> GetRoles();
    Task<bool> ChangeRole(ChangeRoleDTO changeRoleDTO);
    Task<GetActualAccountDTO?> GetActualAccount(string userName);
    Task<string?> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO);
    Task<string?> UpdateAccount(UpdateAccountDTO updateUserDTO);
}
