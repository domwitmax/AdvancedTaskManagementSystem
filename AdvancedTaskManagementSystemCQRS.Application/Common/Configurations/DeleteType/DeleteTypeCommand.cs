using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeleteType;

public record DeleteTypeCommand(int Id) : IRequest<bool>;
