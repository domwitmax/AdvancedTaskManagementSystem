using AdvancedTaskManagementSystemCQRS.Application.Common.ChangesHistory.GetChangesHistoryByTaskId;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CanAccessProject;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetTaskById;
using AdvancedTaskManagementSystemCQRS.Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTaskManagementSystemCQRS.WebApi.ChangesHistory;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ChangesHistoryController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetChangesHistoryByTaskId/{taskId}")]
    public async Task<IActionResult> GetChangesHistoryByTaskId([FromRoute] int taskId)
    {
        var task = await mediator.Send(new GetTaskByIdQuery(taskId));
        if (task == null)
            return NotFound();
        if (!await mediator.Send(new CanAccessProjectQuery(
                task.ProjectId,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        return Ok(await mediator.Send(new GetChangesHistoryByTaskIdQuery(taskId)));
    }

}
