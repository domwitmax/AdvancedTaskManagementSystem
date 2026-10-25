using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.ChangesHistory;

public class ChangesHistoryService(IChangesHistoryRepository changesHistoryRepository) : IChangesHistoryService
{
    public Task<List<ChangesHistoryEntityDTO>> GetChangesHistoryByTaskId(int taskId)
    {
        return changesHistoryRepository.GetChangesHistoryByTaskId(taskId);
    }
}
