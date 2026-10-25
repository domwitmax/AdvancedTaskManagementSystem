using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Services.Account;
using AdvancedTaskManagementSystemCRUD.Application.Services.Account.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.PasswordHasher;
using AdvancedTaskManagementSystemCRUD.Application.Services.Token;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.Application.Services;

public class AccountService(
    IAccountRepository userRepository,
    IPasswordHasher passwordHasher,
    ITokenService tokenService,
    IMapper mapper
    ) : IAccountService
{
    public async Task<string?> ChangePasswordAsync(ChangePasswordDTO changePasswordDTO)
    {
        var user = await userRepository.GetAccount(changePasswordDTO.Login);
        if (user == null)
            return null;

        if (!passwordHasher.Verify(changePasswordDTO.OldPassword, user.PasswordHash))
            return null;

        user.PasswordHash = passwordHasher.Hash(changePasswordDTO.NewPassword);
        user.ChangePasswordAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
        userRepository.UpdateAccount(user);
        await userRepository.SaveChangesAsync();
        return tokenService.GenerateToken(user);
    }

    public async Task<GetActualAccountDTO?> GetActualAccount(string userName)
    {
        var user = await userRepository.GetAccount(userName);
        return mapper.Map<GetActualAccountDTO?>(user);
    }

    public async Task<GetAccountDTO?> GetAccount(string userName)
    {
        var user = await userRepository.GetAccount(userName);
        return mapper.Map<GetAccountDTO?>(user);
    }

    public async Task<List<GetAccountDTO>> GetAccounts()
    {
        var users = await userRepository.GetAccounts();
        return mapper.Map<List<GetAccountDTO>>(users);
    }

    public async Task<List<RoleDTO>> GetRoles()
    {
        var roles = await userRepository.GetRoles();
        return mapper.Map<List<RoleDTO>>(roles);
    }

    public Task<bool> ChangeRole(ChangeRoleDTO changeRoleDTO)
        => userRepository.ChangeRole(changeRoleDTO.Login, changeRoleDTO.RoleId);

    public async Task<string?> Login(LoginDTO loginDTO)
    {
        var user = await userRepository.GetAccount(loginDTO.Login);
        if (user == null || !user.Active || !passwordHasher.Verify(loginDTO.Password, user.PasswordHash))
            return null;

        user.LastLoginAt = DateTime.UtcNow;
        userRepository.UpdateAccount(user);
        await userRepository.SaveChangesAsync();
        return tokenService.GenerateToken(user);
    }

    public async Task<string?> Register(RegisterDTO registerDTO)
    {
        var now = DateTime.UtcNow;
        var user = mapper.Map<Domain.Entities.AccountEntity>(registerDTO);
        user.PasswordHash = passwordHasher.Hash(registerDTO.Password);
        user.CreatedAt = now;
        user.UpdatedAt = now;
        user.LastLoginAt = now;
        user.LastGetUserAt = now;
        user.Active = true;
        user.RoleId = await userRepository.HasAnyAccounts() ? 3 : 1;

        userRepository.AddAccount(user);
        await userRepository.SaveChangesAsync();

        user = await userRepository.GetAccount(registerDTO.Login);
        return user == null ? null : tokenService.GenerateToken(user);
    }

    public async Task<string?> UpdateAccount(UpdateAccountDTO updateUserDTO)
    {
        var userFromDb = await userRepository.GetAccount(updateUserDTO.Login);
        if (userFromDb == null)
            return null;

        var user = mapper.Map<Domain.Entities.AccountEntity>((updateUserDTO, userFromDb));
        userRepository.UpdateAccount(user);
        await userRepository.SaveChangesAsync();

        user = await userRepository.GetAccount(user.Login);
        return user == null ? null : tokenService.GenerateToken(user);
    }
}
