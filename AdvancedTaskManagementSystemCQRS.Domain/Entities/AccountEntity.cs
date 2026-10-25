namespace AdvancedTaskManagementSystemCQRS.Domain.Entities;

public class AccountEntity
{
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? ChangePasswordAt { get; set; }
    public DateTime LastLoginAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime LastGetUserAt { get; set; }
    public bool Active { get; set; }
    public int RoleId { get; set; }
    public virtual RoleEntity Role { get; set; } = null!;
    public virtual ICollection<ProjectMemberEntity> ProjectMemberships { get; set; } = new List<ProjectMemberEntity>();

    public void UpdateAccount(AccountEntity updatedAccount)
    {
        if (!string.IsNullOrEmpty(updatedAccount.Email))
            Email = updatedAccount.Email;
        if (!string.IsNullOrEmpty(updatedAccount.PasswordHash))
            PasswordHash = updatedAccount.PasswordHash;
        UpdatedAt = DateTime.UtcNow;
        if (updatedAccount.Active != Active)
            Active = updatedAccount.Active;
    }
}
