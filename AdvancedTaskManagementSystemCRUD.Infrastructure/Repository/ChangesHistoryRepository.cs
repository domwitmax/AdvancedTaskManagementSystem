using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;
using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.Repository
{
    public class ChangesHistoryRepository(TaskManagerDbContext dbContext, IMapper mapper) : IChangesHistoryRepository
    {
        public async Task<int> AddChangesHistory(ChangesHistoryEntityDTO changesHistoryEntity)
        {
            var entity = mapper.Map<ChangesHistoryEntity>(changesHistoryEntity);
            dbContext.ChangesHistory.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<List<ChangesHistoryEntityDTO>> GetChangesHistoryByTaskId(int taskId)
        {
            var entities = await dbContext.ChangesHistory
                .Include(ch => ch.ChangeDetails)
                .Where(ch => ch.TaskId == taskId)
                .OrderByDescending(ch => ch.ChangedAt)
                .ThenByDescending(ch => ch.Id)
                .ToListAsync();

            return mapper.Map<List<ChangesHistoryEntityDTO>>(entities);
        }
    }
}
