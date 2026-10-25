using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.DeleteLabel;

public record DeleteLabelCommand(int Id) : IRequest<bool>;
