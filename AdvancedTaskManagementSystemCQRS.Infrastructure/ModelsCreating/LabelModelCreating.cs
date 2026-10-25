using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.ModelsCreating;

public static class LabelModelCreating
{
    public static void AddLabelModelCreating(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LabelEntity>(entity =>
        {
            entity.ToTable("Label");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasData(
                new LabelEntity { Id = 1, Name = "bug", Description = "Błąd wymagający poprawy" },
                new LabelEntity { Id = 2, Name = "feature", Description = "Nowa funkcjonalność" },
                new LabelEntity { Id = 3, Name = "dokumentacja", Description = "Zadanie dotyczące dokumentacji" },
                new LabelEntity { Id = 4, Name = "pilne", Description = "Zadanie o wysokiej pilności" },
                new LabelEntity { Id = 5, Name = "opcjonalne", Description = "Zadanie opcjonalne" }
            );
        });
    }
}
