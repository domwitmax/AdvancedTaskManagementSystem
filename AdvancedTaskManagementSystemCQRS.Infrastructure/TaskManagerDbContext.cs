using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AdvancedTaskManagementSystemCQRS.Infrastructure.ModelsCreating;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure;

public class TaskManagerDbContext : DbContext
{
    public TaskManagerDbContext(DbContextOptions<TaskManagerDbContext> options) : base(options) { }
    public DbSet<AccountEntity> Account { get; set; }
    public DbSet<TaskEntity> Task { get; set; }
    public DbSet<TypeEntity> Type { get; set; }
    public DbSet<StatusEntity> Status { get; set; }
    public DbSet<GroupEntity> Groups { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
    public DbSet<PriorityEntity> Priorities { get; set; }
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<ProjectStatusEntity> ProjectStatuses { get; set; }
    public DbSet<ChangeDetailEntity> ChangeDetails { get; set; }
    public DbSet<ChangesHistoryEntity> ChangesHistory { get; set; }
    public DbSet<LabelEntity> Labels { get; set; }
    public DbSet<TaskLabelEntity> TaskLabels { get; set; }
    public DbSet<TaskAttachmentEntity> TaskAttachments { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<ProjectMemberEntity> ProjectMembers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.AddUserModelCreating();
        modelBuilder.AddRoleModelCreating();
        modelBuilder.AddProjectMemberModelCreating();
        modelBuilder.AddTaskModelCreating();
        modelBuilder.AddTypeModelCreating();
        modelBuilder.AddStatusModelCreating();
        modelBuilder.AddGroupModelCreating();
        modelBuilder.AddCommentModelCreating();
        modelBuilder.AddPriorityModelCreating();
        modelBuilder.AddProjectModelCreating();
        modelBuilder.AddProjectStatusModelCreating();
        modelBuilder.AddChangeDetailModelCreating();
        modelBuilder.AddChangesHistoryModelCreating();
        modelBuilder.AddLabelModelCreating();
        modelBuilder.AddTaskLabelModelCreating();
        modelBuilder.AddTaskAttachmentModelCreating();
    }
}
