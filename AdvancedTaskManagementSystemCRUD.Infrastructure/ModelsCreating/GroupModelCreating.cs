using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.ModelsCreating
{
    public static class GroupModelCreating
    {
        public static void AddGroupModelCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupEntity>(entity =>
            {
                entity.ToTable("Group");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
            });
        }
    }
}
