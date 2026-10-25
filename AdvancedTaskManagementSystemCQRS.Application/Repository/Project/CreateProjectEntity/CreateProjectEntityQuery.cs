using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Project.CreateProjectEntity;

public class CreateProjectEntityCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int StatusId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string CreatedBy { get; set; } = null!;
}
