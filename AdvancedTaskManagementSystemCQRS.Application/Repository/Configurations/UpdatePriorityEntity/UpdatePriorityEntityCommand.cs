using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdatePriorityEntity;

public record UpdatePriorityEntityCommand(int Id, string Name) : IRequest<int?>;