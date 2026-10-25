namespace AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs
{
    public class ChangeDetailEntityDTO
    {
        public int Id { get; set; }
        public int ChangesHistoryId { get; set; }
        public string PropertyName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}
