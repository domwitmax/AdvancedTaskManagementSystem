using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.ModelsCreating;

public static class TaskLabelModelCreating
{
    public static void AddTaskLabelModelCreating(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskLabelEntity>(entity =>
        {
            entity.ToTable("TaskLabel");
            entity.HasKey(e => new { e.TaskId, e.LabelId });
            entity.HasOne(e => e.Task)
                .WithMany(e => e.TaskLabels)
                .HasForeignKey(e => e.TaskId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(e => e.Label)
                .WithMany(e => e.TaskLabels)
                .HasForeignKey(e => e.LabelId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
