using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;
using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.Repository
{
    public class CommentRepository(TaskManagerDbContext dbContext, IMapper mapper) : ICommentRepository
    {
        public async Task<int?> AddComment(CommentDTO commentDTO)
        {
            var taskExists = await dbContext.Task.AnyAsync(item => item.Id == commentDTO.TaskId);
            if (!taskExists)
                return null;

            var comment = mapper.Map<CommentEntity>(commentDTO);
            comment.CreatedAt = DateTime.UtcNow;
            dbContext.Comments.Add(comment);
            await dbContext.SaveChangesAsync();
            return comment.Id;
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            var comment = await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if (comment == null) return false;

            dbContext.Comments.Remove(comment);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<CommentDTO?> GetCommentById(int commentId)
        {
            var comment = await dbContext.Comments
                .AsNoTracking()
                .FirstOrDefaultAsync(item => item.Id == commentId);
            return mapper.Map<CommentDTO?>(comment);
        }

        public async Task<List<CommentDTO>> GetCommentsById(int taskId)
        {
            var comments = await dbContext.Comments
                .Where(c => c.TaskId == taskId)
                .OrderBy(c => c.CreatedAt)
                .ThenBy(c => c.Id)
                .ToListAsync();
            return mapper.Map<List<CommentDTO>>(comments);
        }

        public async Task<int?> UpdateCommentContent(CommentDTO commentDTO)
        {
            var comment = await dbContext.Comments.FirstOrDefaultAsync(c => c.Id == commentDTO.Id);
            if (comment == null) return null;

            comment.Content = commentDTO.Content;
            await dbContext.SaveChangesAsync();
            return comment.Id;
        }
    }
}
