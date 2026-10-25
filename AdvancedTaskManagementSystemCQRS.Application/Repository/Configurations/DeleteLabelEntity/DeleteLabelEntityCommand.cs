using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DeleteLabelEntity;

public record DeleteLabelEntityCommand(int Id) : IRequest<bool>;
