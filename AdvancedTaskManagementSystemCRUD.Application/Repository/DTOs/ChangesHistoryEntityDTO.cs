namespace AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs
{
    public class ChangesHistoryEntityDTO
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string ChangedBy { get; set; }
        public List<ChangeDetailEntityDTO> ChangeDetails { get; set; }
        public DateTime ChangedAt { get; set; }
    }
}
