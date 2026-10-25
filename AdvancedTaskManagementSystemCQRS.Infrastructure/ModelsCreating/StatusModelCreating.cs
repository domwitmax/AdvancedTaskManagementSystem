using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.ModelsCreating;

public static class StatusModelCreating
{
        public static void AddStatusModelCreating(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatusEntity>(entity =>
            {
                entity.ToTable("Status");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.TypeId).IsRequired();
                entity.Property(e => e.IsFinished).IsRequired();
                entity.HasOne(e => e.Type)
                    .WithMany()
                    .HasForeignKey(e => e.TypeId)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasData(
                    new StatusEntity { Id = 1, Name = "Nowy", TypeId = 1, IsFinished = false },
                    new StatusEntity { Id = 2, Name = "W trakcie", TypeId = 1, IsFinished = false },
                    new StatusEntity { Id = 3, Name = "Testy", TypeId = 1, IsFinished = false },
                    new StatusEntity { Id = 4, Name = "Przetestowany", TypeId = 1, IsFinished = false },
                    new StatusEntity { Id = 5, Name = "Zakończony", TypeId = 1, IsFinished = true },
                    new StatusEntity { Id = 6, Name = "Zawieszony", TypeId = 1, IsFinished = false },
                    new StatusEntity { Id = 7, Name = "Propozycja", TypeId = 2, IsFinished = false },
                    new StatusEntity { Id = 8, Name = "Akceptacja", TypeId = 2, IsFinished = false },
                    new StatusEntity { Id = 9, Name = "Odrzucony", TypeId = 2, IsFinished = true },
                    new StatusEntity { Id = 10, Name = "Nowy", TypeId = 2, IsFinished = false },
                    new StatusEntity { Id = 11, Name = "Anulowany", TypeId = 1, IsFinished = true }
                );
            });
    }
}
