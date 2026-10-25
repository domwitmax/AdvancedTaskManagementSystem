using AdvancedTaskManagementSystemCRUD.Domain.Entities;

namespace AdvancedTaskManagementSystemCRUD.Application.Repository;

public interface IAccountRepository
{
    Task<AccountEntity?> GetAccount(string userName);
    Task<List<AccountEntity>> GetAccounts();
    Task<List<RoleEntity>> GetRoles();
    Task<bool> HasAnyAccounts();
    Task<bool> ChangeRole(string login, int roleId);
    void AddAccount(AccountEntity user);
    void UpdateAccount(AccountEntity user);
    bool DeactiveAccount(string userName);
    Task SaveChangesAsync();
    bool IsLoginTaken(string login);
    bool IsEmailTaken(string email);
    bool IsEmailTakenExcept(string email, string login);
}
