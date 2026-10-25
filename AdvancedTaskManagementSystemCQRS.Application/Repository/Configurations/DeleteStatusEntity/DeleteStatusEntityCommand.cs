using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DeleteStatus;

public record DeleteStatusEntityCommand(int Id) : IRequest<bool>;