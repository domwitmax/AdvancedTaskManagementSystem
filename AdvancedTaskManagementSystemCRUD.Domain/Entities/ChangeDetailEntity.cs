namespace AdvancedTaskManagementSystemCRUD.Domain.Entities;

public class ChangeDetailEntity
{
    public int Id { get; set; }
    public int ChangesHistoryId { get; set; }
    public string PropertyName { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
}