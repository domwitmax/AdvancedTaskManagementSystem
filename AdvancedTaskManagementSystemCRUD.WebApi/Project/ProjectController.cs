using AdvancedTaskManagementSystemCRUD.Application.Services.Project;
using AdvancedTaskManagementSystemCRUD.Application.Services.Authorization;
using AdvancedTaskManagementSystemCRUD.Application.Services.Project.DTOs;
using AdvancedTaskManagementSystemCRUD.Domain.Constants;
using AdvancedTaskManagementSystemCRUD.WebApi.Extensions;
using AdvancedTaskManagementSystemCRUD.WebApi.Request;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTaskManagementSystemCRUD.WebApi.Project;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProjectController(
    IProjectService projectService,
    IProjectAccessService projectAccessService,
    IMapper mapper,
    IValidator<ProjectDTO> projectValidator,
    IValidator<AddProjectMemberDTO> addProjectMemberValidator) : ControllerBase
{
    [Authorize(Roles = RoleNames.ProjectManagement)]
    [HttpPost("CreateProject")]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectRequest request)
    {
        var userName = User.GetLogin();
        if (userName == null)
            return BadRequest("Nie udało się odczytać loginu zalogowanego użytkownika z tokenu JWT.");
        var projectDTO = mapper.Map<ProjectDTO>(request);
        var validationResult = await projectValidator.ValidateAsync(projectDTO);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        var projectId = await projectService.CreateProject(projectDTO, userName);
        return projectId == 0 ? BadRequest("Nie udało się utworzyć projektu. Sprawdź status projektu oraz dane wejściowe.") : CreatedAtAction(nameof(GetProjectById), new { id = projectId }, projectId);
    }

    [Authorize(Roles = RoleNames.ProjectManagement)]
    [HttpPut("UpdateProject")]
    public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectRequest request)
    {
        if (projectService.GetProjectById(request.Id) is null)
            return NotFound();
        if (!projectAccessService.CanAccessProject(
                request.Id,
                User.GetLogin(),
                User.IsInRole(RoleNames.Administrator)))
            return Forbid();
        var projectDTO = mapper.Map<ProjectDTO>(request);
        var validationResult = await projectValidator.ValidateAsync(projectDTO, options =>
        {
            options.IncludeRulesNotInRuleSet();
            options.IncludeRuleSets("Update");
        });
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        var projectId = await projectService.UpdateProject(projectDTO);
        return projectId == 0 ? NotFound() : Ok(projectId);
    }

    [Authorize(Roles = RoleNames.ProjectManagement)]
    [HttpDelete("DeleteProject/{id}")]
    public async Task<IActionResult> DeleteProject([FromRoute] int id)
    {
        if (projectService.GetProjectById(id) is null)
            return NotFound();
        if (!projectAccessService.CanAccessProject(
                id,
                User.GetLogin(),
                User.IsInRole(RoleNames.Administrator)))
            return Forbid();
        var result = await projectService.DeleteProject(id);
        return result switch
        {
            0 => NotFound(),
            -1 => Conflict("Nie można usunąć projektu, ponieważ zawiera zadania."),
            _ => Ok()
        };
    }

    [HttpGet("GetProjectById/{id}")]
    public IActionResult GetProjectById([FromRoute] int id)
    {
        var projectDTO = projectService.GetProjectById(id);
        if (projectDTO is null)
            return NotFound();
        return projectAccessService.CanAccessProject(
            id,
            User.GetLogin(),
            User.IsInRole(RoleNames.Administrator))
            ? Ok(projectDTO)
            : Forbid();
    }

    [HttpGet("GetAllProjects")]
    public IActionResult GetAllProjects()
    {
        var userName = User.GetLogin();
        if (userName == null)
            return BadRequest("Nie udało się odczytać loginu zalogowanego użytkownika z tokenu JWT.");

        return Ok(projectAccessService.GetAccessibleProjects(
            userName,
            User.IsInRole(RoleNames.Administrator)));
    }

    [HttpGet("GetProjectsByUser/{userName}")]
    public IActionResult GetProjectsByUser([FromRoute] string userName)
    {
        var currentUser = User.GetLogin();
        if (currentUser == null)
            return BadRequest("Nie udało się odczytać loginu zalogowanego użytkownika z tokenu JWT.");
        if (!projectAccessService.CanViewUserProjects(
                currentUser,
                userName,
                User.IsInRole(RoleNames.Administrator)))
            return Forbid();
        return Ok(projectService.GetProjectsByUser(userName));
    }

    [HttpGet("GetProjectMembers/{projectId}")]
    public IActionResult GetProjectMembers([FromRoute] int projectId)
    {
        if (projectService.GetProjectById(projectId) is null)
            return NotFound();
        return projectAccessService.CanAccessProject(
            projectId,
            User.GetLogin(),
            User.IsInRole(RoleNames.Administrator))
            ? Ok(projectService.GetProjectMembers(projectId))
            : Forbid();
    }

    [Authorize(Roles = RoleNames.ProjectManagement)]
    [HttpPost("AddProjectMember")]
    public async Task<IActionResult> AddProjectMember([FromBody] AddProjectMemberRequest request)
    {
        var userName = User.GetLogin();
        if (userName == null)
            return BadRequest("Nie udało się odczytać loginu zalogowanego użytkownika z tokenu JWT.");
        if (projectService.GetProjectById(request.ProjectId) is null)
            return NotFound();
        if (!projectAccessService.CanAccessProject(
                request.ProjectId,
                User.GetLogin(),
                User.IsInRole(RoleNames.Administrator)))
            return Forbid();
        var dto = mapper.Map<AddProjectMemberDTO>(request);
        dto.AddedBy = userName;
        var validationResult = await addProjectMemberValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);
        return await projectService.AddProjectMember(dto) ? Ok() : BadRequest("Nie można dodać członka projektu. Użytkownik może już należeć do projektu albo dane są niepoprawne.");
    }

    [Authorize(Roles = RoleNames.ProjectManagement)]
    [HttpDelete("DeleteProjectMember/{projectId}/{userLogin}")]
    public async Task<IActionResult> DeleteProjectMember([FromRoute] int projectId, [FromRoute] string userLogin)
    {
        if (projectService.GetProjectById(projectId) is null)
            return NotFound();
        if (!projectAccessService.CanAccessProject(
                projectId,
                User.GetLogin(),
                User.IsInRole(RoleNames.Administrator)))
            return Forbid();
        return await projectService.DeleteProjectMember(projectId, userLogin)
            ? Ok()
            : BadRequest("Nie można usunąć członka, ponieważ nie istnieje albo ma przypisane zadania w projekcie.");
    }

}
