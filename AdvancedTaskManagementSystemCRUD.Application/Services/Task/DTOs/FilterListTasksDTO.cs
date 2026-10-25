namespace AdvancedTaskManagementSystemCRUD.Application.Services.Task.DTOs;

public class FilterListTasksDTO
{
    public int? ByStatusId { get; set; }
    public int? ByTypeId { get; set; }
    public string? ByPriority { get; set; }
    public string? ByCreator { get; set; }
    public string? ByAssignedTo { get; set; }
    public string? ByLabel { get; set; }
    public DateTime? BetweenCreatedBegin { get; set; }
    public DateTime? BetweenCreatedEnd { get; set; }
    public DateTime? BetweenUpdatedBegin { get; set; }
    public DateTime? BetweenUpdatedEnd { get; set; }
    public DateTime? BetweenDueDateBegin { get; set; }
    public DateTime? BetweenDueDateEnd { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
}
