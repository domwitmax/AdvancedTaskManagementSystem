using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DeleteTypeEntity;

public record DeleteTypeEntityCommand(int Id) : IRequest<bool>;