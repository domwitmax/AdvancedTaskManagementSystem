using AdvancedTaskManagementSystemCRUD.Application.Services.Comments.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Comment
{
    public class CommentService(ICommentRepository commentRepository, IMapper mapper) : ICommentService
    {
        public async Task<int?> AddComment(CommentDTO comment)
        {
            var commentEntity = mapper.Map<Repository.DTOs.CommentDTO>(comment);
            return await commentRepository.AddComment(commentEntity);
        }

        public Task<int?> UpdateComment(CommentDTO comment)
        {
            var commentEntity = mapper.Map<Repository.DTOs.CommentDTO>(comment);
            return commentRepository.UpdateCommentContent(commentEntity);
        }

        public async Task<CommentDTO?> GetCommentById(int commentId)
        {
            var comment = await commentRepository.GetCommentById(commentId);
            return mapper.Map<CommentDTO?>(comment);
        }

        public async Task<List<CommentDTO>> GetCommentsByTaskId(int taskId)
        {
            var commentEntities = await commentRepository.GetCommentsById(taskId);
            return mapper.Map<List<CommentDTO>>(commentEntities);
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            return await commentRepository.DeleteComment(commentId);
        }
    }
}
