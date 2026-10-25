using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DeleteGroupEntity;

public record DeleteGroupEntityCommand(string Name) : IRequest<bool>;