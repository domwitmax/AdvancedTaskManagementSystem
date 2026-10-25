namespace AdvancedTaskManagementSystemCRUD.Application.Repository.Configurations.DTOs
{
    public class StatusEntityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public bool IsFinished { get; set; }
    }
}
