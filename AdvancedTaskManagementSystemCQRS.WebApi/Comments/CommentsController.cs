using AdvancedTaskManagementSystemCQRS.Application.Common.Comments.AddComment;
using AdvancedTaskManagementSystemCQRS.Application.Common.Comments.DeleteComment;
using AdvancedTaskManagementSystemCQRS.Application.Common.Comments.GetCommentById;
using AdvancedTaskManagementSystemCQRS.Application.Common.Comments.GetCommentsByTaskId;
using AdvancedTaskManagementSystemCQRS.Application.Common.Comments.UpdateContentComment;
using AdvancedTaskManagementSystemCQRS.Application.Common.Comments.CanModifyComment;
using AdvancedTaskManagementSystemCQRS.Application.Common.Projects.CanAccessProject;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.GetTaskById;
using AdvancedTaskManagementSystemCQRS.Domain.Constants;
using AdvancedTaskManagementSystemCQRS.WebApi.Request;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTaskManagementSystemCQRS.WebApi.Comments;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommentsController(IMediator mediator, IMapper mapper) : ControllerBase
{
    [HttpPost("AddComment")]
    public async Task<IActionResult> AddComment([FromBody] AddCommentRequest request)
    {
        var userName = User.Identity?.Name;
        if (userName is null)
            return BadRequest();
        var task = await mediator.Send(new GetTaskByIdQuery(request.TaskId));
        if (task == null)
            return NotFound();
        if (!await mediator.Send(new CanAccessProjectQuery(
                task.ProjectId,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        var commentId = await mediator.Send(new AddCommentCommand(request.TaskId, request.Content, userName));
        return commentId == null ? NotFound() : Ok(commentId);
    }

    [HttpGet("GetCommentsByTaskId/{taskId}")]
    public async Task<IActionResult> GetCommentsByTaskId([FromRoute] int taskId)
    {
        var task = await mediator.Send(new GetTaskByIdQuery(taskId));
        if (task == null)
            return NotFound();
        return await mediator.Send(new CanAccessProjectQuery(
            task.ProjectId,
            User.Identity?.Name,
            User.IsInRole(RoleNames.Administrator)))
            ? Ok(await mediator.Send(new GetCommentsByTaskIdQuery(taskId)))
            : Forbid();
    }

    [HttpPut("UpdateContentComment")]
    public async Task<IActionResult> UpdateContentComment([FromBody] UpdateContentCommentRequest request)
    {
        var comment = await mediator.Send(new GetCommentByIdQuery(request.CommentId));
        if (comment == null)
            return NotFound();
        if (!await mediator.Send(new CanModifyCommentQuery(
                comment.CreatedBy,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        var task = await mediator.Send(new GetTaskByIdQuery(comment.TaskId));
        if (task == null)
            return NotFound();
        if (!await mediator.Send(new CanAccessProjectQuery(
                task.ProjectId,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        var result = await mediator.Send(mapper.Map<UpdateContentCommentCommand>(request));
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("DeleteComment/{commentId}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
    {
        var comment = await mediator.Send(new GetCommentByIdQuery(commentId));
        if (comment == null)
            return NotFound();
        if (!await mediator.Send(new CanModifyCommentQuery(
                comment.CreatedBy,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        var task = await mediator.Send(new GetTaskByIdQuery(comment.TaskId));
        if (task == null)
            return NotFound();
        if (!await mediator.Send(new CanAccessProjectQuery(
                task.ProjectId,
                User.Identity?.Name,
                User.IsInRole(RoleNames.Administrator))))
            return Forbid();
        return await mediator.Send(new DeleteCommentCommand(commentId)) ? Ok() : NotFound();
    }

}
