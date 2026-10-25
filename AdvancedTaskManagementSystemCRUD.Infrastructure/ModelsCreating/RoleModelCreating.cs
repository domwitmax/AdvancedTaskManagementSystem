using AdvancedTaskManagementSystemCRUD.Domain.Constants;
using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.ModelsCreating;

public static class RoleModelCreating
{
    public static void AddRoleModelCreating(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleEntity>(entity =>
        {
            entity.ToTable("Role");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasData(
                new RoleEntity { Id = 1, Name = RoleNames.Administrator },
                new RoleEntity { Id = 2, Name = RoleNames.ProjectManager },
                new RoleEntity { Id = 3, Name = RoleNames.TeamMember });
        });
    }
}
