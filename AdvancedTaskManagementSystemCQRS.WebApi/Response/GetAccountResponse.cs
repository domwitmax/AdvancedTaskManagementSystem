namespace AdvancedTaskManagementSystemCQRS.WebApi.Account.Response;

public class GetAccountResponse
{
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool Active { get; set; }
    public int RoleId { get; set; }
    public string RoleName { get; set; } = null!;
}
