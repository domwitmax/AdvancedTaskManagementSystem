using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.ModelsCreating
{
    public static class PriorityModelCreating
    {
        public static void AddPriorityModelCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriorityEntity>(entity =>
            {
                entity.ToTable("Priority");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasData(
                    new PriorityEntity { Id = 1, Name = "Niski" },
                    new PriorityEntity { Id = 2, Name = "Średni" },
                    new PriorityEntity { Id = 3, Name = "Wysoki" }
                    );
            });
        }
    }
}
