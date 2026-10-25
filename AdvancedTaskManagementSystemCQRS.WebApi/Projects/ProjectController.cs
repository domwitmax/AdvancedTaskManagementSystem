using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.AddProjectMember;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CanAccessProject;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CreateProject;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DeleteProject;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.DeleteProjectMember;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetAccessibleProjects;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetProjectById;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetProjectMembers;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.GetProjectsByUser;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CanViewUserProjects;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.UpdateProject;
using AdvancedTaskManagementSystemCQRS.Domain.Constants;
using AdvancedTaskManagementSystemCQRS.WebApi.Projects.Request;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTaskManagementSystemCQRS.WebApi.Projects;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProjectController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [Authorize(Roles = RoleNames.ProjectManagement)]
    [HttpPost("CreateProject")]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
    {
        var userName = User.Identity?.Name;
        if (userName == null)
            return BadRequest("Nie udało się odczytać loginu zalogowanego użytkownika z tokenu JWT."); ;
        var command = mapper.Map<CreateProjectCommand>(request);
        command.CreatedBy = userName;
        var projectId = await mediator.Send(command);
        return projectId == 0 ? BadRequest("Nie udało się utworzyć projektu. Sprawdź status projektu oraz dane wejściowe.") : CreatedAtAction(nameof(GetProjectById), new { id = projectId }, projectId);
    }

    [Authorize(Roles = RoleNames.ProjectManagement)]
    [HttpPut("UpdateProject")]
    public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectRequest request)
    {
        if (await mediator.Send(new GetProjectByIdQuery(request.Id)) is null)
            return NotFound();
        if (!await mediator.Send(new CanAccessProjectQuery(
                request.Id,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        var result = await mediator.Send(mapper.Map<UpdateProjectCommand>(request));
        return result is null ? NotFound() : Ok(result);
    }

    [Authorize(Roles = RoleNames.ProjectManagement)]
    [HttpDelete("DeleteProject/{id}")]
    public async Task<IActionResult> DeleteProject([FromRoute] int id)
    {
        if (await mediator.Send(new GetProjectByIdQuery(id)) is null)
            return NotFound();
        if (!await mediator.Send(new CanAccessProjectQuery(
                id,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        var result = await mediator.Send(new DeleteProjectCommand(id));
        return result switch
        {
            0 => NotFound(),
            -1 => Conflict("Nie można usunąć projektu, ponieważ zawiera zadania."),
            _ => Ok()
        };
    }

    [HttpGet("GetAllProjects")]
    public async Task<IActionResult> GetAllProjects()
    {
        var userName = User.Identity?.Name;
        if (userName == null)
            return BadRequest();

        return Ok(await mediator.Send(new GetAccessibleProjectsQuery(
            userName,
            User.IsInRole(RoleNames.Administrator))));
    }

    [HttpGet("GetProjectById/{id}")]
    public async Task<IActionResult> GetProjectById([FromRoute] int id)
    {
        var project = await mediator.Send(new GetProjectByIdQuery(id));
        if (project == null)
            return NotFound();
        if (!await mediator.Send(new CanAccessProjectQuery(
                id,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        return Ok(project);
    }

    [HttpGet("GetProjectsByUser/{userName}")]
    public async Task<IActionResult> GetProjectsByUser([FromRoute] string userName)
    {
        var currentUser = User.Identity?.Name;
        if (currentUser == null)
            return BadRequest();
        if (!await mediator.Send(new CanViewUserProjectsQuery(
                currentUser,
                userName,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        return Ok(await mediator.Send(new GetProjectsByUserQuery(userName)));
    }

    [HttpGet("GetProjectMembers/{projectId}")]
    public async Task<IActionResult> GetProjectMembers([FromRoute] int projectId)
    {
        if (await mediator.Send(new GetProjectByIdQuery(projectId)) is null)
            return NotFound();
        if (!await mediator.Send(new CanAccessProjectQuery(
                projectId,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        return Ok(await mediator.Send(new GetProjectMembersQuery(projectId)));
    }

    [Authorize(Roles = RoleNames.ProjectManagement)]
    [HttpPost("AddProjectMember")]
    public async Task<IActionResult> AddProjectMember([FromBody] AddProjectMemberRequest request)
    {
        var userName = User.Identity?.Name;
        if (userName == null)
            return BadRequest();
        if (await mediator.Send(new GetProjectByIdQuery(request.ProjectId)) is null)
            return NotFound();
        if (!await mediator.Send(new CanAccessProjectQuery(
                request.ProjectId,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        var result = await mediator.Send(new AddProjectMemberCommand(request.ProjectId, request.UserLogin, userName));
        return result ? Ok() : BadRequest();
    }

    [Authorize(Roles = RoleNames.ProjectManagement)]
    [HttpDelete("DeleteProjectMember/{projectId}/{userLogin}")]
    public async Task<IActionResult> DeleteProjectMember([FromRoute] int projectId, [FromRoute] string userLogin)
    {
        if (await mediator.Send(new GetProjectByIdQuery(projectId)) is null)
            return NotFound();
        if (!await mediator.Send(new CanAccessProjectQuery(
                projectId,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        var result = await mediator.Send(new DeleteProjectMemberCommand(projectId, userLogin));
        return result ? Ok() : BadRequest("Nie można usunąć członka, ponieważ nie istnieje albo ma przypisane zadania w projekcie.");
    }

}
