using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.ModelsCreating;

public static class ProjectStatusModelCreating
{
        public static void AddProjectStatusModelCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectStatusEntity>(entity =>
            {
                entity.ToTable("ProjectStatus");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasData(
                    new ProjectStatusEntity { Id = 1, Name = "Nowy" },
                    new ProjectStatusEntity { Id = 2, Name = "Aktywny" },
                    new ProjectStatusEntity { Id = 3, Name = "Zakończony" },
                    new ProjectStatusEntity { Id = 4, Name = "Archiwalny"}
                );
            });
    }
}
