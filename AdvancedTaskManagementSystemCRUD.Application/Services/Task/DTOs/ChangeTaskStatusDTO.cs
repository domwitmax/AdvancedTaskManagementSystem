namespace AdvancedTaskManagementSystemCRUD.Application.Services.Task.DTOs;

public class ChangeTaskStatusDTO
{
    public int Id { get; set; }
    public int StatusId { get; set; }
    public string ChangedBy { get; set; }
}
