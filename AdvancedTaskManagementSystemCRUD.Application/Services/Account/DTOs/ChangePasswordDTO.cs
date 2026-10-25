namespace AdvancedTaskManagementSystemCRUD.Application.Services.Account.DTOs;

public class ChangePasswordDTO
{
    public string Login { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}
