using AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.ChangesHistory;

public interface IChangesHistoryService
{
    Task<List<ChangesHistoryEntityDTO>> GetChangesHistoryByTaskId(int taskId);
}
