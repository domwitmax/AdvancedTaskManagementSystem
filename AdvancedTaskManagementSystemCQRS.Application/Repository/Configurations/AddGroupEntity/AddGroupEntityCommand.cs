using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddGroupEntity;

public record AddGroupEntityCommand(string Name, string Description) : IRequest<int>;