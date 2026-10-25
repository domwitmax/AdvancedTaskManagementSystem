using AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;

namespace AdvancedTaskManagementSystemCRUD.Application.Repository
{
    public interface IChangesHistoryRepository
    {
        Task<int> AddChangesHistory(ChangesHistoryEntityDTO changesHistoryEntity);
        Task<List<ChangesHistoryEntityDTO>> GetChangesHistoryByTaskId(int taskId);
    }
}
