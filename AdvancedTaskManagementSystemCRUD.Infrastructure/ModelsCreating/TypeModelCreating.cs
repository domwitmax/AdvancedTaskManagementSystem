using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.ModelsCreating
{
    public static class TypeModelCreating
    {
        public static void AddTypeModelCreating(this ModelBuilder modelBuilder)
        {
                modelBuilder.Entity<TypeEntity>(entity =>
                {
                    entity.ToTable("Type");
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                    entity.HasData(
                        new TypeEntity { Id = 1, Name = "Zadanie" },
                        new TypeEntity { Id = 2, Name = "Negocjacje" }
                    );
                });
        }
    }
}
