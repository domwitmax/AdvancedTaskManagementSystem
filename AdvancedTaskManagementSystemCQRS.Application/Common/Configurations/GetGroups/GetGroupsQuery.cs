using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Configurations.GetGroups;

public record GetGroupsQuery() : IRequest<List<string>>;
