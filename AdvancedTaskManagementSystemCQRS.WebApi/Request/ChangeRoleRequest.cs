namespace AdvancedTaskManagementSystemCQRS.WebApi.Account.Request;

public class ChangeRoleRequest
{
    public string Login { get; set; } = null!;
    public int RoleId { get; set; }
}
