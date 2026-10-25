namespace AdvancedTaskManagementSystemCRUD.Application.Services.Task.DTOs;

public class AssignmentTaskDTO
{
    public int Id { get; set; }
    public string AssignedTo { get; set; }
    public string ChangedBy { get; set; }
}
