using AdvancedTaskManagementSystemCRUD.Application.Services.Comments.DTOs;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Comment
{
    public interface ICommentService
    {
        Task<int?> AddComment(CommentDTO comment);
        Task<int?> UpdateComment(CommentDTO comment);
        Task<List<CommentDTO>> GetCommentsByTaskId(int taskId);
        Task<CommentDTO?> GetCommentById(int commentId);
        Task<bool> DeleteComment(int commentId);
    }
}
