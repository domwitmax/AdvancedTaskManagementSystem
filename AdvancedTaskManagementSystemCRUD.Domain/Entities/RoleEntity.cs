namespace AdvancedTaskManagementSystemCRUD.Domain.Entities;

public class RoleEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<AccountEntity> Accounts { get; set; } = new List<AccountEntity>();
}
