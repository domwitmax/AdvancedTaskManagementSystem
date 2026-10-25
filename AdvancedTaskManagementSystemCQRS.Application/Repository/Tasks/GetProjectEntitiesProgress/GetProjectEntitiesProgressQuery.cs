using AdvancedTaskManagementSystemCQRS.Application.Common.Reports.DTOs;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetProjectEntitiesProgress;

public record GetProjectEntitiesProgressQuery(int ProjectId) : IRequest<ProjectProgressDTO?>;
