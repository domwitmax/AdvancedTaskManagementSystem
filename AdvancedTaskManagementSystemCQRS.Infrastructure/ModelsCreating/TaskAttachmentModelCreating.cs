using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.ModelsCreating;

public static class TaskAttachmentModelCreating
{
    public static void AddTaskAttachmentModelCreating(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskAttachmentEntity>(entity =>
        {
            entity.ToTable("TaskAttachment");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FileName).IsRequired().HasMaxLength(255);
            entity.Property(e => e.ContentType).IsRequired().HasMaxLength(200);
            entity.Property(e => e.FileSize).IsRequired();
            entity.Property(e => e.Content).IsRequired();
            entity.Property(e => e.UploadedBy).IsRequired().HasMaxLength(50);
            entity.Property(e => e.UploadedAt).IsRequired();

            entity.HasOne(e => e.Task)
                .WithMany(e => e.Attachments)
                .HasForeignKey(e => e.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
