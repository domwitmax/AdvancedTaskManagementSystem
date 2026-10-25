using AdvancedTaskManagementSystemCRUD.Application.Services.Authorization;
using AdvancedTaskManagementSystemCRUD.Application.Services.Task.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Task;
using AdvancedTaskManagementSystemCRUD.Domain.Constants;
using AdvancedTaskManagementSystemCRUD.WebApi.Request;
using AdvancedTaskManagementSystemCRUD.WebApi.Extensions;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTaskManagementSystemCRUD.WebApi.Tasks;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaskController(
    ITaskService taskService,
    IProjectAccessService projectAccessService,
    IMapper mapper,
    IValidator<TaskDTO> taskValidator,
    IValidator<AssignmentTaskDTO> assignmentTaskValidator,
    IValidator<ChangeTaskStatusDTO> changeTaskStatusValidator) : ControllerBase
{
    [HttpPost("CreateTask")]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
        var userName = User.GetLogin();
        if (userName is null)
            return BadRequest("Nie udało się odczytać loginu zalogowanego użytkownika z tokenu JWT.");
        var task = mapper.Map<TaskDTO>(request);
        task.CreatedBy = userName;
        var validation = await taskValidator.ValidateAsync(task);
        if (!validation.IsValid)
            return BadRequest(validation.Errors);
        var taskId = await taskService.CreateTask(task);
        return taskId == 0 ? BadRequest("Nie udało się utworzyć zadania. Sprawdź projekt, przypisanie, status, typ, priorytet i etykiety.") : CreatedAtAction(nameof(GetTaskById), new { Id = taskId }, taskId);
    }

    [HttpPut("EditTask")]
    public async Task<IActionResult> EditTask([FromBody] EditTaskRequest request)
    {
        var userName = User.GetLogin();
        if (userName is null)
            return BadRequest("Nie udało się odczytać loginu zalogowanego użytkownika z tokenu JWT.");
        var task = mapper.Map<TaskDTO>(request);
        task.ChangedBy = userName;
        var validation = await taskValidator.ValidateAsync(task);
        if (!validation.IsValid)
            return BadRequest(validation.Errors);
        return await taskService.EditTask(task, userName) == 0 ? NotFound() : Ok();
    }

    [HttpGet("GetListAllTasks")]
    public async Task<IActionResult> GetListAllTasks()
    {
        return Ok(projectAccessService.FilterAccessibleTasks(
            await taskService.GetAllTasks(),
            User.GetLogin(),
            User.IsInRole(RoleNames.Administrator)));
    }

    [HttpGet("GetListTasksByAssignment/{Assignment}")]
    public async Task<IActionResult> GetListTasksByAssignment([FromRoute] string Assignment)
    {
        return Ok(projectAccessService.FilterAccessibleTasks(
            await taskService.GetTaskByAssignment(Assignment),
            User.GetLogin(),
            User.IsInRole(RoleNames.Administrator)));
    }

    [HttpGet("GetListTasksByCreator/{Creator}")]
    public async Task<IActionResult> GetListTasksByCreator([FromRoute] string Creator)
    {
        return Ok(projectAccessService.FilterAccessibleTasks(
            await taskService.GetTaskByCreator(Creator),
            User.GetLogin(),
            User.IsInRole(RoleNames.Administrator)));
    }

    [HttpGet("GetTaskById/{Id}")]
    public async Task<IActionResult> GetTaskById([FromRoute] int Id)
    {
        var task = await taskService.GetTaskById(Id);
        if (task == null)
            return NotFound();
        return projectAccessService.CanAccessProject(
            task.ProjectId,
            User.GetLogin(),
            User.IsInRole(RoleNames.Administrator))
            ? Ok(task)
            : Forbid();
    }

    [HttpDelete("DeleteTaskById/{Id}")]
    public async Task<IActionResult> DeleteTaskById([FromRoute] int Id)
    {
        var task = await taskService.GetTaskById(Id);
        if (task == null)
            return NotFound();
        if (!projectAccessService.CanAccessProject(
                task.ProjectId,
                User.GetLogin(),
                User.IsInRole(RoleNames.Administrator)))
            return Forbid();
        return await taskService.DeleteTask(Id) ? Ok() : NotFound();
    }

    [HttpPost("AssignmentTask")]
    public async Task<IActionResult> AssignmentTask([FromBody] AssignmentTaskRequest request)
    {
        var userName = User.GetLogin();
        if (userName is null)
            return BadRequest("Nie udało się odczytać loginu zalogowanego użytkownika z tokenu JWT.");
        var dto = mapper.Map<AssignmentTaskDTO>(request);
        dto.ChangedBy = userName;
        var validation = await assignmentTaskValidator.ValidateAsync(dto);
        if (!validation.IsValid)
            return BadRequest(validation.Errors);
        return await taskService.AssignmentTask(dto, userName) == 0 ? NotFound() : Ok();
    }

    [HttpPost("ChangeTaskStatus")]
    public async Task<IActionResult> ChangeTaskStatus([FromBody] ChangeTaskStatusRequest request)
    {
        var userName = User.GetLogin();
        if (userName is null)
            return BadRequest("Nie udało się odczytać loginu zalogowanego użytkownika z tokenu JWT.");
        var dto = mapper.Map<ChangeTaskStatusDTO>(request);
        dto.ChangedBy = userName;
        var validation = await changeTaskStatusValidator.ValidateAsync(dto);
        if (!validation.IsValid)
            return BadRequest(validation.Errors);
        return await taskService.ChangeTaskStatus(dto, userName) == 0 ? NotFound() : Ok();
    }

    [HttpPost("AddAttachment/{taskId}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> AddAttachment([FromRoute] int taskId, IFormFile file)
    {
        var userName = User.GetLogin();
        if (userName is null)
            return BadRequest("Nie udało się odczytać loginu zalogowanego użytkownika z tokenu JWT.");
        var task = await taskService.GetTaskById(taskId);
        if (task == null)
            return NotFound();
        if (!projectAccessService.CanAccessProject(
                task.ProjectId,
                User.GetLogin(),
                User.IsInRole(RoleNames.Administrator)))
            return Forbid();
        if (file == null || file.Length == 0)
            return BadRequest("Plik jest wymagany.");
        if (file.Length > 10 * 1024 * 1024)
            return BadRequest("Maksymalny rozmiar pliku to 10 MB.");

        await using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var attachment = mapper.Map<TaskAttachmentDTO>(file);
        attachment.TaskId = taskId;
        attachment.Content = memoryStream.ToArray();
        attachment.UploadedBy = userName;
        var attachmentId = await taskService.AddAttachment(attachment);
        return attachmentId == null ? NotFound() : Ok(attachmentId);
    }

    [HttpGet("GetAttachmentsByTaskId/{taskId}")]
    public async Task<IActionResult> GetAttachmentsByTaskId([FromRoute] int taskId)
    {
        var task = await taskService.GetTaskById(taskId);
        if (task == null)
            return NotFound();
        return projectAccessService.CanAccessProject(
            task.ProjectId,
            User.GetLogin(),
            User.IsInRole(RoleNames.Administrator))
            ? Ok(await taskService.GetAttachmentsByTaskId(taskId))
            : Forbid();
    }

    [HttpGet("DownloadAttachment/{attachmentId}")]
    public async Task<IActionResult> DownloadAttachment([FromRoute] int attachmentId)
    {
        var attachment = await taskService.GetAttachmentById(attachmentId);
        if (attachment == null || attachment.Content == null)
            return NotFound();
        var task = await taskService.GetTaskById(attachment.TaskId);
        if (task == null)
            return NotFound();
        if (!projectAccessService.CanAccessProject(
                task.ProjectId,
                User.GetLogin(),
                User.IsInRole(RoleNames.Administrator)))
            return Forbid();
        return File(attachment.Content, attachment.ContentType, attachment.FileName);
    }

    [HttpPost("FilterListTasks")]
    public async Task<IActionResult> FilterListTasks([FromBody] FilterListTasksRequest request)
    {
        return Ok(projectAccessService.FilterAccessibleTasks(
            await taskService.GetFilterListTasks(mapper.Map<FilterListTasksDTO>(request)),
            User.GetLogin(),
            User.IsInRole(RoleNames.Administrator)));
    }

}
