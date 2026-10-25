namespace AdvancedTaskManagementSystemCRUD.WebApi.Account.Request;

public class ChangePasswordRequest
{
    public string Login { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
