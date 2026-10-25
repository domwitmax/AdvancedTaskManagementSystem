using AdvancedTaskManagementSystemCRUD.Domain.Entities;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Token;

public interface ITokenService
{
    string GenerateToken(Domain.Entities.AccountEntity user);
}
