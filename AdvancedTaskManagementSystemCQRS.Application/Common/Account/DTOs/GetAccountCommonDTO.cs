namespace AdvancedTaskManagementSystemCQRS.Application.Common.Account.DTOs;

public class GetAccountCommonDTO
{
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool Active { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; } = null!;
}
