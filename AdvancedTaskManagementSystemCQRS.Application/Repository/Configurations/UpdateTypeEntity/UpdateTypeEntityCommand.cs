using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdateTypeEntity;

public record UpdateTypeEntityCommand(int Id, string Name) : IRequest<int?>;