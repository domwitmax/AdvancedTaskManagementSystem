namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DTOs;

public class StatusDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TypeId { get; set; }
    public bool IsFinished { get; set; }
}
