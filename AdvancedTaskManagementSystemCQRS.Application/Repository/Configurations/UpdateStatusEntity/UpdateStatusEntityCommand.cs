using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdateStatusEntity;

public record UpdateStatusEntityCommand(int Id, string Name, bool IsFinished) : IRequest<int?>;