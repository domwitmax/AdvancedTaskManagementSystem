namespace AdvancedTaskManagementSystemCRUD.Application.Services.Reports.DTOs;

public class ProjectProgressDTO
{
    public int ProjectId { get; set; }
    public int AllTasks { get; set; }
    public int CompletedTasks { get; set; }
}
