using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.ModelsCreating;

public static class UserModelCreating
{
    public static void AddUserModelCreating(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountEntity>(entity =>
        {
            entity.ToTable("Account");
            entity.HasKey(e => e.Login);
            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.LastLoginAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
            entity.Property(e => e.LastGetUserAt).IsRequired();
            entity.Property(e => e.RoleId).IsRequired();
            entity.HasOne(e => e.Role)
                .WithMany(e => e.Accounts)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
