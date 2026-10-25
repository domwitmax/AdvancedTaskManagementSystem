namespace AdvancedTaskManagementSystemCRUD.Domain.Entities;

public class StatusEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TypeId { get; set; }
    public bool IsFinished { get; set; }
    public virtual TypeEntity Type { get; set; }
}
