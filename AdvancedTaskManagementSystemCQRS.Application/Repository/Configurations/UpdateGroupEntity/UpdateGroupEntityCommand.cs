using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdateGroupEntity;

public record UpdateGroupEntityCommand(string oldName, string newName) : IRequest<int?>;