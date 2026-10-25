namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;

public class TaskChangeDetailDTO
{
    public string PropertyName { get; set; } = string.Empty;
    public string OldValue { get; set; } = string.Empty;
    public string NewValue { get; set; } = string.Empty;
}
