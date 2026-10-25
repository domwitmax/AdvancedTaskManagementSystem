using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddPriorityEntity;

public record AddPriorityEntityCommand(string Name) : IRequest<int>;