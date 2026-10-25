using AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;

namespace AdvancedTaskManagementSystemCRUD.Application.Repository
{
    public interface ICommentRepository
    {
        Task<int?> AddComment(CommentDTO commentDTO);
        Task<int?> UpdateCommentContent(CommentDTO commentDTO);
        Task<List<CommentDTO>> GetCommentsById(int taskId);
        Task<CommentDTO?> GetCommentById(int commentId);
        Task<bool> DeleteComment(int commentId);
    }
}
