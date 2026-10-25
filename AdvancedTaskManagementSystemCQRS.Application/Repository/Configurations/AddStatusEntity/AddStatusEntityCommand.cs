using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddStatusEntity;

public record AddStatusEntityCommand(string Name, int TypeId, bool IsFinished) : IRequest<int>;