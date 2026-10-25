namespace AdvancedTaskManagementSystemCQRS.Domain.Entities;

public class ChangesHistoryEntity
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public string ChangedBy { get; set; }
    public List<ChangeDetailEntity> ChangeDetails { get; set; }
    public DateTime ChangedAt { get; set; }
    public TaskEntity Task { get; set; }
    public AccountEntity Account { get; set; }
}
