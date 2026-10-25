using AdvancedTaskManagementSystemCRUD.Application.Services.Comment;
using AdvancedTaskManagementSystemCRUD.Application.Services.Comments.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Authorization;
using AdvancedTaskManagementSystemCRUD.Application.Services.Task;
using AdvancedTaskManagementSystemCRUD.Domain.Constants;
using AdvancedTaskManagementSystemCRUD.WebApi.Request;
using AdvancedTaskManagementSystemCRUD.WebApi.Extensions;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTaskManagementSystemCRUD.WebApi.Comments;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommentsController(
    ICommentService commentService,
    ITaskService taskService,
    IProjectAccessService projectAccessService,
    IMapper mapper,
    IValidator<CommentDTO> commentValidator) : ControllerBase
{
    [HttpPost("AddComment")]
    public async Task<IActionResult> AddComment([FromBody] AddCommentRequest request)
    {
        var userName = User.GetLogin();
        if (userName is null)
            return BadRequest("Nie udało się odczytać loginu zalogowanego użytkownika z tokenu JWT.");
        var task = await taskService.GetTaskById(request.TaskId);
        if (task == null)
            return NotFound();
        if (!projectAccessService.CanAccessProject(
                task.ProjectId,
                User.GetLogin(),
                User.IsInRole(RoleNames.Administrator)))
            return Forbid();
        var comment = mapper.Map<CommentDTO>(request);
        comment.CreatedBy = userName;
        var validation = await commentValidator.ValidateAsync(comment);
        if (!validation.IsValid)
            return BadRequest(validation.Errors);
        var commentId = await commentService.AddComment(comment);
        return commentId == null ? NotFound() : Ok(commentId);
    }

    [HttpGet("GetCommentsByTaskId/{taskId}")]
    public async Task<IActionResult> GetCommentsByTaskId([FromRoute] int taskId)
    {
        var task = await taskService.GetTaskById(taskId);
        if (task == null)
            return NotFound();
        return projectAccessService.CanAccessProject(
            task.ProjectId,
            User.GetLogin(),
            User.IsInRole(RoleNames.Administrator))
            ? Ok(await commentService.GetCommentsByTaskId(taskId))
            : Forbid();
    }

    [HttpPut("UpdateContentComment")]
    public async Task<IActionResult> UpdateContentComment([FromBody] UpdateContentCommentRequest request)
    {
        var existing = await commentService.GetCommentById(request.CommentId);
        if (existing == null)
            return NotFound();
        if (!projectAccessService.CanModifyComment(
                existing.CreatedBy,
                User.GetLogin(),
                User.IsInRole(RoleNames.Administrator)))
            return Forbid();
        var task = await taskService.GetTaskById(existing.TaskId);
        if (task == null)
            return NotFound();
        if (!projectAccessService.CanAccessProject(
                task.ProjectId,
                User.GetLogin(),
                User.IsInRole(RoleNames.Administrator)))
            return Forbid();
        var comment = mapper.Map<CommentDTO>(request);
        var validation = await commentValidator.ValidateAsync(comment);
        if (!validation.IsValid)
            return BadRequest(validation.Errors);
        var result = await commentService.UpdateComment(comment);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpDelete("DeleteComment/{commentId}")]
    public async Task<IActionResult> DeleteComment([FromRoute] int commentId)
    {
        var existing = await commentService.GetCommentById(commentId);
        if (existing == null)
            return NotFound();
        if (!projectAccessService.CanModifyComment(
                existing.CreatedBy,
                User.GetLogin(),
                User.IsInRole(RoleNames.Administrator)))
            return Forbid();
        var task = await taskService.GetTaskById(existing.TaskId);
        if (task == null)
            return NotFound();
        if (!projectAccessService.CanAccessProject(
                task.ProjectId,
                User.GetLogin(),
                User.IsInRole(RoleNames.Administrator)))
            return Forbid();
        return await commentService.DeleteComment(commentId) ? Ok() : NotFound();
    }

}
