using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.ModelsCreating
{
    public static class ProjectModelCreating
    {
        public static void AddProjectModelCreating(this ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<ProjectEntity>(entity =>
                {
                    entity.ToTable("Project");
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                    entity.Property(e => e.Description).HasMaxLength(500);
                    entity.Property(e => e.StartDate).IsRequired();
                    entity.Property(e => e.StatusId).IsRequired();
                    entity.HasOne(e => e.Status)
                        .WithMany()
                        .HasForeignKey(e => e.StatusId);
                });
        }
    }
}
