using AdvancedTaskManagementSystemCQRS.Application.Common.Reports.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Reports.GetProjectProgress;

public record GetProjectProgressQuery(int ProjectId) : IRequest<ProjectProgressDTO?>;
