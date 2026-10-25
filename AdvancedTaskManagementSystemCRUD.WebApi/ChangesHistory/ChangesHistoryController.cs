using AdvancedTaskManagementSystemCRUD.Application.Services.ChangesHistory;
using AdvancedTaskManagementSystemCRUD.Application.Services.Authorization;
using AdvancedTaskManagementSystemCRUD.Application.Services.Task;
using AdvancedTaskManagementSystemCRUD.Domain.Constants;
using AdvancedTaskManagementSystemCRUD.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTaskManagementSystemCRUD.WebApi.ChangesHistory;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ChangesHistoryController(
    IChangesHistoryService changesHistoryService,
    ITaskService taskService,
    IProjectAccessService projectAccessService) : ControllerBase
{
    [HttpGet("GetChangesHistoryByTaskId/{taskId}")]
    public async Task<IActionResult> GetChangesHistoryByTaskId([FromRoute] int taskId)
    {
        var task = await taskService.GetTaskById(taskId);
        if (task == null)
            return NotFound();
        if (!projectAccessService.CanAccessProject(
                task.ProjectId,
                User.GetLogin(),
                User.IsInRole(RoleNames.Administrator)))
            return Forbid();
        return Ok(await changesHistoryService.GetChangesHistoryByTaskId(taskId));
    }

}
