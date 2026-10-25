namespace AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DTOs;

public class ProjectDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int StatusId { get; set; }
    public string StatusName { get; set; }
}
