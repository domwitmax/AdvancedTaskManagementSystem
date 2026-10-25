using AdvancedTaskManagementSystemCRUD.Application.Services.Project;
using AdvancedTaskManagementSystemCRUD.Application.Services.Authorization;
using AdvancedTaskManagementSystemCRUD.Application.Services.Reports;
using AdvancedTaskManagementSystemCRUD.Domain.Constants;
using AdvancedTaskManagementSystemCRUD.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTaskManagementSystemCRUD.WebApi.Reports;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ReportsController(
    IReportService reportService,
    IProjectService projectService,
    IProjectAccessService projectAccessService) : ControllerBase
{
    [Authorize(Roles = RoleNames.Administrator)]
    [HttpGet("GetTasksByStatus")]
    public async Task<IActionResult> GetTasksByStatus() => Ok(await reportService.GetTasksByStatus());

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpGet("GetDelayedTasks")]
    public async Task<IActionResult> GetDelayedTasks() => Ok(await reportService.GetDelayedTasks());

    [HttpGet("GetUserTasks/{userName}")]
    public async Task<IActionResult> GetUserTasks([FromRoute] string userName)
    {
        var currentUser = User.GetLogin();
        if (currentUser == null)
            return BadRequest("Nie udało się odczytać loginu zalogowanego użytkownika z tokenu JWT.");
        if (!projectAccessService.CanViewUserProjects(
                currentUser,
                userName,
                User.IsInRole(RoleNames.Administrator)))
            return Forbid();
        return Ok(await reportService.GetUserTasks(userName));
    }

    [HttpGet("GetProjectProgress/{projectId}")]
    public async Task<IActionResult> GetProjectProgress([FromRoute] int projectId)
    {
        if (projectService.GetProjectById(projectId) is null)
            return NotFound();
        if (!projectAccessService.CanAccessProject(
                projectId,
                User.GetLogin(),
                User.IsInRole(RoleNames.Administrator)))
            return Forbid();
        var progress = await reportService.GetProjectProgress(projectId);
        return progress is null ? NotFound() : Ok(progress);
    }

    [Authorize(Roles = RoleNames.Administrator)]
    [HttpGet("GetUserWorkload")]
    public async Task<IActionResult> GetUserWorkload() => Ok(await reportService.GetUserWorkload());

}
