using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddTypeEntity;

public record AddTypeEntityCommand(string Name) : IRequest<int>;