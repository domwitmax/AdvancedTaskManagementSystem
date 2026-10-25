using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetProjectById;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CanAccessProject;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CanViewUserProjects;
using AdvancedTaskManagementSystemCQRS.Application.Common.Reports.GetDelayedTasks;
using AdvancedTaskManagementSystemCQRS.Application.Common.Reports.GetProjectProgress;
using AdvancedTaskManagementSystemCQRS.Application.Common.Reports.GetTasksByStatus;
using AdvancedTaskManagementSystemCQRS.Application.Common.Reports.GetUserTasks;
using AdvancedTaskManagementSystemCQRS.Application.Common.Reports.GetUserWorkload;
using AdvancedTaskManagementSystemCQRS.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTaskManagementSystemCQRS.WebApi.Reports;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReportsController(IMediator mediator) : ControllerBase
{
    [Authorize(Roles = RoleNames.Administrator)]
    [HttpGet("GetTasksByStatus")]
    public async Task<IActionResult> GetTasksByStatus() => Ok(await mediator.Send(new GetTasksByStatusQuery()));

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpGet("GetDelayedTasks")]
    public async Task<IActionResult> GetDelayedTasks() => Ok(await mediator.Send(new GetDelayedTasksQuery()));

    [HttpGet("GetUserTasks/{userName}")]
    public async Task<IActionResult> GetUserTasks([FromRoute] string userName)
    {
        var currentUser = User.Identity?.Name;
        if (currentUser == null)
            return BadRequest();
        if (!await mediator.Send(new CanViewUserProjectsQuery(
                currentUser,
                userName,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        return Ok(await mediator.Send(new GetUserTasksQuery(userName)));
    }

    [HttpGet("GetProjectProgress/{projectId}")]
    public async Task<IActionResult> GetProjectProgress([FromRoute] int projectId)
    {
        if (await mediator.Send(new GetProjectByIdQuery(projectId)) is null)
            return NotFound();
        if (!await mediator.Send(new CanAccessProjectQuery(
                projectId,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        var progress = await mediator.Send(new GetProjectProgressQuery(projectId));
        return progress is null ? NotFound() : Ok(progress);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpGet("GetUserWorkload")]
    public async Task<IActionResult> GetUserWorkload() => Ok(await mediator.Send(new GetUserWorkloadQuery()));

}
