namespace AdvancedTaskManagementSystemCQRS.Application.Common.ChangesHistory.DTOs;

public class ChangesHistoryDTO
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public string ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; }
    public List<ChangeDetailDTO> ChangeDetails { get; set; }
}
