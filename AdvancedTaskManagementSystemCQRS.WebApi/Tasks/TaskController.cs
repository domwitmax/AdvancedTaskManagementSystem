using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CanAccessProject;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.AssignmentTask;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.AddTaskAttachment;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetAttachmentById;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetAttachmentsByTaskId;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.ChangeTaskStatus;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.CreateTask;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DeleteTaskById;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.EditTask;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.FilterListTasks;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.FilterAccessibleTasks;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetListAllTasks;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetListTasksByAssignment;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetListTasksByCreator;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetTaskById;
using AdvancedTaskManagementSystemCQRS.Domain.Constants;
using AdvancedTaskManagementSystemCQRS.WebApi.Request;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTaskManagementSystemCQRS.WebApi.Tasks;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TaskController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost("CreateTask")]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
    {
        var userName = User.Identity?.Name;
        if (userName is null)
            return BadRequest("Nie udało się odczytać loginu zalogowanego użytkownika z tokenu JWT.");
        var command = mapper.Map<CreateTaskCommand>(request);
        command.CreatedBy = userName;
        var taskId = await mediator.Send(command);
        return taskId == 0 ? BadRequest("Nie udało się utworzyć zadania. Sprawdź projekt, przypisanie, status, typ, priorytet i etykiety.") : CreatedAtAction(nameof(GetTaskById), new { Id = taskId }, taskId);
    }

    [HttpPut("EditTask")]
    public async Task<IActionResult> EditTask([FromBody] EditTaskRequest request)
    {
        var userName = User.Identity?.Name;
        if (userName is null)
            return BadRequest();
        var command = mapper.Map<EditTaskCommand>(request);
        command.ChangedBy = userName;
        var result = await mediator.Send(command);
        return result == 0 ? NotFound() : Ok();
    }

    [HttpGet("GetListAllTasks")]
    public async Task<IActionResult> GetListAllTasks()
        => Ok(await mediator.Send(new FilterAccessibleTasksQuery(
            await mediator.Send(new GetListAllTasksQuery()),
            User.Identity?.Name,
            User.IsInRole(RoleNames.Administrator))));

    [HttpGet("GetListTasksByAssignment/{Assignment}")]
    public async Task<IActionResult> GetListTasksByAssignment([FromRoute] string Assignment)
        => Ok(await mediator.Send(new FilterAccessibleTasksQuery(
            await mediator.Send(new GetListTasksByAssignmentQuery(Assignment)),
            User.Identity?.Name,
            User.IsInRole(RoleNames.Administrator))));

    [HttpGet("GetListTasksByCreator/{Creator}")]
    public async Task<IActionResult> GetListTasksByCreator([FromRoute] string Creator)
        => Ok(await mediator.Send(new FilterAccessibleTasksQuery(
            await mediator.Send(new GetListTasksByCreatorQuery(Creator)),
            User.Identity?.Name,
            User.IsInRole(RoleNames.Administrator))));

    [HttpGet("GetTaskById/{Id}")]
    public async Task<IActionResult> GetTaskById([FromRoute] int Id)
    {
        var task = await mediator.Send(new GetTaskByIdQuery(Id));
        if (task == null)
            return NotFound();
        return await mediator.Send(new CanAccessProjectQuery(
            task.ProjectId,
            User.Identity?.Name,
            User.IsInRole(RoleNames.Administrator)))
            ? Ok(task)
            : Forbid();
    }

    [HttpDelete("DeleteTaskById/{Id}")]
    public async Task<IActionResult> DeleteTaskById([FromRoute] int Id)
    {
        var task = await mediator.Send(new GetTaskByIdQuery(Id));
        if (task == null)
            return NotFound();
        if (!await mediator.Send(new CanAccessProjectQuery(
                task.ProjectId,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        return await mediator.Send(new DeleteTaskByIdCommand(Id)) ? Ok() : NotFound();
    }

    [HttpPost("AssignmentTask")]
    public async Task<IActionResult> AssignmentTask([FromBody] AssignmentTaskRequest request)
    {
        var userName = User.Identity?.Name;
        if (userName is null)
            return BadRequest();
        var command = mapper.Map<AssignmentTaskCommand>(request);
        command.ChangedBy = userName;
        var result = await mediator.Send(command);
        return result == 0 ? NotFound() : Ok();
    }

    [HttpPost("ChangeTaskStatus")]
    public async Task<IActionResult> ChangeTaskStatus([FromBody] ChangeTaskStatusRequest request)
    {
        var userName = User.Identity?.Name;
        if (userName is null)
            return BadRequest();
        var command = mapper.Map<ChangeTaskStatusCommand>(request);
        command.ChangedBy = userName;
        var result = await mediator.Send(command);
        return result == 0 ? NotFound() : Ok();
    }

    [HttpPost("AddAttachment/{taskId}")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> AddAttachment([FromRoute] int taskId, IFormFile file)
    {
        var userName = User.Identity?.Name;
        if (userName is null)
            return BadRequest();
        var task = await mediator.Send(new GetTaskByIdQuery(taskId));
        if (task == null)
            return NotFound();
        if (!await mediator.Send(new CanAccessProjectQuery(
                task.ProjectId,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
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
        var attachmentId = await mediator.Send(mapper.Map<AddTaskAttachmentCommand>(attachment));
        return attachmentId == null ? NotFound() : Ok(attachmentId);
    }

    [HttpGet("GetAttachmentsByTaskId/{taskId}")]
    public async Task<IActionResult> GetAttachmentsByTaskId([FromRoute] int taskId)
    {
        var task = await mediator.Send(new GetTaskByIdQuery(taskId));
        if (task == null)
            return NotFound();
        if (!await mediator.Send(new CanAccessProjectQuery(
                task.ProjectId,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        return Ok(await mediator.Send(new GetAttachmentsByTaskIdQuery(taskId)));
    }

    [HttpGet("DownloadAttachment/{attachmentId}")]
    public async Task<IActionResult> DownloadAttachment([FromRoute] int attachmentId)
    {
        var attachment = await mediator.Send(new GetAttachmentByIdQuery(attachmentId));
        if (attachment == null || attachment.Content == null)
            return NotFound();
        var task = await mediator.Send(new GetTaskByIdQuery(attachment.TaskId));
        if (task == null)
            return NotFound();
        if (!await mediator.Send(new CanAccessProjectQuery(
                task.ProjectId,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        return File(attachment.Content, attachment.ContentType, attachment.FileName);
    }

    [HttpPost("FilterListTasks")]
    public async Task<IActionResult> FilterListTasks([FromBody] FilterListTasksRequest request)
        => Ok(await mediator.Send(new FilterAccessibleTasksQuery(
            await mediator.Send(mapper.Map<FilterListTasksQuery>(request)),
            User.Identity?.Name,
            User.IsInRole(RoleNames.Administrator))));

}
