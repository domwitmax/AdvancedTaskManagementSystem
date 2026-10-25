using AdvancedTaskManagementSystemCQRS.Application.Common.Reports.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Reports.GetUserWorkload;

public record GetUserWorkloadQuery() : IRequest<List<UserWorkloadDTO>>;
