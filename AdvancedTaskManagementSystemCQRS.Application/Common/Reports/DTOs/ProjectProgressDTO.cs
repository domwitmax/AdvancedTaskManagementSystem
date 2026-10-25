namespace AdvancedTaskManagementSystemCQRS.Application.Common.Reports.DTOs
{
    public record ProjectProgressDTO(int ProjectId, int AllTasks, int CompletedTasks);
}