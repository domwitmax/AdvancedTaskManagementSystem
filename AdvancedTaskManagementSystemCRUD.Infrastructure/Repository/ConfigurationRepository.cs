using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Repository.Configurations.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;
using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCRUD.Infrastructure.Repository
{
    public class ConfigurationRepository(TaskManagerDbContext dbContext, IMapper mapper) : IConfigurationRepository
    {
        public async Task<int> AddGroup(GroupEntityDTO groupEntity)
        {
            var group = mapper.Map<GroupEntity>(groupEntity);
            dbContext.Groups.Add(group);
            await dbContext.SaveChangesAsync();
            return group.Id;
        }
        
        public async Task<int> AddPriority(PriorityEntityDTO priorityEntity)
        {
            var priority = mapper.Map<PriorityEntity>(priorityEntity);
            dbContext.Priorities.Add(priority);
            await dbContext.SaveChangesAsync();
            return priority.Id;
        }

        public async Task<int> AddStatus(StatusEntityDTO statusEntity)
        {
            var status = mapper.Map<StatusEntity>(statusEntity);
            dbContext.Status.Add(status);
            await dbContext.SaveChangesAsync();
            return status.Id;
        }

        public async Task<int> AddProjectStatus(ProjectStatusEntityDTO projectStatusEntity)
        {
            var projectStatus = mapper.Map<ProjectStatusEntity>(projectStatusEntity);
            dbContext.ProjectStatuses.Add(projectStatus);
            await dbContext.SaveChangesAsync();
            return projectStatus.Id;
        }

        public async Task<int> AddType(TypeEntityDTO typeEntity)
        {
            var type = mapper.Map<TypeEntity>(typeEntity);
            dbContext.Type.Add(type);
            await dbContext.SaveChangesAsync();
            return type.Id;
        }

        public async Task<int> AddLabel(LabelEntityDTO labelEntity)
        {
            var normalizedName = labelEntity.Name.Trim();
            if (await dbContext.Labels.AnyAsync(item => item.Name.ToLower() == normalizedName.ToLower()))
                return 0;

            var label = mapper.Map<LabelEntity>(labelEntity);
            label.Name = normalizedName;
            dbContext.Labels.Add(label);
            await dbContext.SaveChangesAsync();
            return label.Id;
        }

        public async Task<int?> UpdateGroup(string oldName, string newName)
        {
            var group = dbContext.Groups.FirstOrDefault(g => g.Name == oldName);
            if (group == null) return null;
            group.Name = newName;
            await dbContext.SaveChangesAsync();
            return group.Id;
        }

        public async Task<int?> UpdatePriority(PriorityEntityDTO priorityEntity)
        {
            var priority = dbContext.Priorities.FirstOrDefault(p => p.Id == priorityEntity.Id);
            if (priority == null) return null;
            priority.Name = priorityEntity.Name;
            await dbContext.SaveChangesAsync();
            return priority.Id;
        }

        public async Task<int?> UpdateStatus(StatusEntityDTO statusEntity)
        {
            var status = dbContext.Status.FirstOrDefault(s => s.Id == statusEntity.Id);
            if (status == null) return null;
            status.Name = statusEntity.Name;
            status.IsFinished = statusEntity.IsFinished;
            await dbContext.SaveChangesAsync();
            return status.Id;
        }

        public async Task<int?> UpdateProjectStatus(ProjectStatusEntityDTO projectStatusEntity)
        {
            var projectStatus = dbContext.ProjectStatuses.FirstOrDefault(ps => ps.Id == projectStatusEntity.Id);
            if (projectStatus == null) return null;
            projectStatus.Name = projectStatusEntity.Name;
            await dbContext.SaveChangesAsync();
            return projectStatus.Id;
        }

        public async Task<int?> UpdateType(TypeEntityDTO typeEntity)
        {
            var type = dbContext.Type.FirstOrDefault(t => t.Id == typeEntity.Id);
            if (type == null) return null;
            type.Name = typeEntity.Name;
            await dbContext.SaveChangesAsync();
            return type.Id;
        }

        public async Task<int?> UpdateLabel(LabelEntityDTO labelEntity)
        {
            var label = await dbContext.Labels.FirstOrDefaultAsync(item => item.Id == labelEntity.Id);
            if (label == null)
                return null;

            var normalizedName = labelEntity.Name.Trim();
            if (await dbContext.Labels.AnyAsync(item =>
                    item.Id != labelEntity.Id && item.Name.ToLower() == normalizedName.ToLower()))
                return 0;

            label.Name = normalizedName;
            label.Description = labelEntity.Description;
            await dbContext.SaveChangesAsync();
            return label.Id;
        }

        public List<string> GetGroups()
        {
            return dbContext.Groups
                .AsNoTracking()
                .OrderBy(item => item.Name)
                .Select(item => item.Name)
                .ToList();
        }

        public List<PriorityEntityDTO> GetPriorities()
        {
            var priorities = dbContext.Priorities.ToList();
            return mapper.Map<List<PriorityEntityDTO>>(priorities);
        }

        public List<StatusEntityDTO> GetStatusByTypeId(int typeId)
        {
            var statuses = dbContext.Status.Where(s => s.TypeId == typeId).ToList();
            return mapper.Map<List<StatusEntityDTO>>(statuses);
        }

        public List<ProjectStatusEntityDTO> GetProjectStatuses()
        {
            var projectStatuses = dbContext.ProjectStatuses.ToList();
            return mapper.Map<List<ProjectStatusEntityDTO>>(projectStatuses);
        }

        public List<TypeEntityDTO> GetTypes()
        {
            var types = dbContext.Type.ToList();
            return mapper.Map<List<TypeEntityDTO>>(types);
        }

        public List<LabelEntityDTO> GetLabels()
        {
            var labels = dbContext.Labels.ToList();
            return mapper.Map<List<LabelEntityDTO>>(labels);
        }

        public async Task<bool> DeleteGroup(string name)
        {
            var group = dbContext.Groups.FirstOrDefault(g => g.Name == name);
            if (group == null) return false;

            var hasTasks = dbContext.Task.Any(t => t.AssignedTo == group.Name);
            if (hasTasks) return false;

            dbContext.Groups.Remove(group);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePriority(int id)
        {
            var priority = dbContext.Priorities.FirstOrDefault(p => p.Id == id);
            if (priority == null) return false;

            var hasTasks = dbContext.Task.Any(t => t.PriorityId == id);
            if (hasTasks) return false;

            dbContext.Priorities.Remove(priority);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteStatus(int id)
        {
            var status = dbContext.Status.FirstOrDefault(s => s.Id == id);
            if (status == null) return false;

            var hasTasks = dbContext.Task.Any(t => t.StatusId == id);
            if (hasTasks) return false;

            dbContext.Status.Remove(status);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProjectStatus(int id)
        {
            var projectStatus = dbContext.ProjectStatuses.FirstOrDefault(ps => ps.Id == id);
            if (projectStatus == null) return false;

            var hasProjects = dbContext.Projects.Any(p => p.StatusId == id);
            if (hasProjects) return false;

            dbContext.ProjectStatuses.Remove(projectStatus);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteType(int id)
        {
            var type = dbContext.Type.FirstOrDefault(t => t.Id == id);
            if (type == null) return false;

            var hasStatuses = dbContext.Status.Any(s => s.TypeId == id);
            var hasTasks = dbContext.Task.Any(t => t.TypeId == id);
            if (hasStatuses || hasTasks) return false;

            dbContext.Type.Remove(type);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLabel(int id)
        {
            var label = dbContext.Labels.FirstOrDefault(l => l.Id == id);
            if (label == null) return false;

            var hasTaskLabels = dbContext.TaskLabels.Any(tl => tl.LabelId == id);
            if (hasTaskLabels) return false;

            dbContext.Labels.Remove(label);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
