using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.ModelsCreating
{
    public static class ChangesHistoryModelCreating
    {
        public static void AddChangesHistoryModelCreating(this ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<ChangesHistoryEntity>(entity =>
                {
                    entity.ToTable("ChangesHistory");
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.ChangedBy).IsRequired().HasMaxLength(50);
                    entity.Property(e => e.ChangedAt).IsRequired();
                    entity.HasMany(e => e.ChangeDetails)
                          .WithOne()
                          .HasForeignKey(cd => cd.ChangesHistoryId)
                          .OnDelete(DeleteBehavior.Cascade);
                    entity.HasOne(e => e.Task)
                          .WithMany()
                          .HasForeignKey(e => e.TaskId)
                          .OnDelete(DeleteBehavior.Cascade);
                    entity.HasOne(e => e.Account)
                          .WithMany()
                          .HasForeignKey(e => e.ChangedBy)
                          .OnDelete(DeleteBehavior.Restrict);
                });
        }
    }
}
