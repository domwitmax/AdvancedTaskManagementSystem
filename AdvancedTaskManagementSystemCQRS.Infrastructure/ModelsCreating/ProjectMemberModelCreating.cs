using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.ModelsCreating;

public static class ProjectMemberModelCreating
{
    public static void AddProjectMemberModelCreating(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectMemberEntity>(entity =>
        {
            entity.ToTable("ProjectMember");
            entity.HasKey(e => new { e.ProjectId, e.AccountLogin });
            entity.Property(e => e.AccountLogin).HasMaxLength(50);
            entity.Property(e => e.AddedAt).IsRequired();
            entity.Property(e => e.AddedBy).IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.Project)
                .WithMany(e => e.ProjectMembers)
                .HasForeignKey(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Account)
                .WithMany(e => e.ProjectMemberships)
                .HasForeignKey(e => e.AccountLogin)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
