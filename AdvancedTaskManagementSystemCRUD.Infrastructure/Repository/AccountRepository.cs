using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.Repository;

public class AccountRepository(TaskManagerDbContext dbContext) : IAccountRepository
{
    public void AddAccount(AccountEntity user)
    {
        dbContext.Account.Add(user);

        if (user.RoleId == 1)
        {
            var projectIds = dbContext.Projects
                .Where(project => !project.ProjectMembers.Any())
                .Select(project => project.Id)
                .ToList();

            dbContext.ProjectMembers.AddRange(projectIds.Select(projectId => new ProjectMemberEntity
            {
                ProjectId = projectId,
                AccountLogin = user.Login,
                AddedAt = user.CreatedAt,
                AddedBy = user.Login
            }));
        }
    }

    public bool DeactiveAccount(string userName)
    {
        var user = dbContext.Account.FirstOrDefault(item => item.Login == userName);
        if (user == null)
            return false;

        user.Active = false;
        user.UpdatedAt = DateTime.UtcNow;
        dbContext.Account.Update(user);
        return true;
    }

    public async Task<AccountEntity?> GetAccount(string userName)
    {
        return await dbContext.Account
            .AsNoTracking()
            .Include(item => item.Role)
            .FirstOrDefaultAsync(item => item.Login == userName);
    }

    public async Task<List<AccountEntity>> GetAccounts()
    {
        return await dbContext.Account
            .AsNoTracking()
            .Include(item => item.Role)
            .OrderBy(item => item.Login)
            .ToListAsync();
    }

    public async Task<List<RoleEntity>> GetRoles()
    {
        return await dbContext.Roles.AsNoTracking().OrderBy(item => item.Id).ToListAsync();
    }

    public Task<bool> HasAnyAccounts()
        => dbContext.Account.AnyAsync();

    public async Task<bool> ChangeRole(string login, int roleId)
    {
        var account = await dbContext.Account.FirstOrDefaultAsync(item => item.Login == login);
        if (account == null || !await dbContext.Roles.AnyAsync(item => item.Id == roleId))
            return false;

        if (account.RoleId == 1 && roleId != 1)
        {
            var administratorsCount = await dbContext.Account.CountAsync(item => item.RoleId == 1);
            if (administratorsCount <= 1)
                return false;
        }

        account.RoleId = roleId;
        account.UpdatedAt = DateTime.UtcNow;
        await dbContext.SaveChangesAsync();
        return true;
    }

    public void UpdateAccount(AccountEntity user)
    {
        if (user.Role != null)
            dbContext.Entry(user.Role).State = EntityState.Unchanged;

        dbContext.Account.Update(user);
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public bool IsLoginTaken(string login)
    {
        return dbContext.Account.Any(item => item.Login == login);
    }

    public bool IsEmailTaken(string email)
    {
        return dbContext.Account.Any(item => item.Email == email);
    }

    public bool IsEmailTakenExcept(string email, string login)
    {
        return dbContext.Account.Any(item => item.Email == email && item.Login != login);
    }
}
