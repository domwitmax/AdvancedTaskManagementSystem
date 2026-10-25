using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.ModelsCreating
{
    public static class ChangeDetailModelCreating
    {
        public static void AddChangeDetailModelCreating(this ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<ChangeDetailEntity>(entity =>
                {
                    entity.ToTable("ChangeDetails");
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.PropertyName).IsRequired().HasMaxLength(100);
                    entity.Property(e => e.OldValue).IsRequired().HasMaxLength(100);
                    entity.Property(e => e.NewValue).IsRequired().HasMaxLength(100);
                });
        }
    }
}
