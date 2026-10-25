using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Repository.Configurations.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Configuration.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Configurations.DTOs;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.Application.Services.Configuration
{
    public class ConfigurationService(IConfigurationRepository configurationRepository, IMapper mapper) : IConfigurationService
    {
        public async Task<int> AddGroup(GroupDTO groupEntity)
        {
            var group = mapper.Map<GroupEntityDTO>(groupEntity);
            return await configurationRepository.AddGroup(group);
        }

        public async Task<int> AddPriority(PriorityDTO priorityEntity)
        {
            var priority = mapper.Map<PriorityEntityDTO>(priorityEntity);
            return await configurationRepository.AddPriority(priority);
        }

        public async Task<int> AddStatus(StatusDTO statusEntity)
        {
            var status = mapper.Map<StatusEntityDTO>(statusEntity);
            return await configurationRepository.AddStatus(status);
        }

        public async Task<int> AddProjectStatus(ProjectStatusDTO projectStatusEntity)
        {
            var projectStatus = mapper.Map<ProjectStatusEntityDTO>(projectStatusEntity);
            return await configurationRepository.AddProjectStatus(projectStatus);
        }

        public async Task<int> AddType(TypeDTO typeEntity)
        {
            var type = mapper.Map<TypeEntityDTO>(typeEntity);
            return await configurationRepository.AddType(type);
        }

        public async Task<int> AddLabel(LabelDTO labelEntity)
        {
            var label = mapper.Map<LabelEntityDTO>(labelEntity);
            return await configurationRepository.AddLabel(label);
        }

        public async Task<int?> UpdateGroup(string oldName, string newName)
        {
            return await configurationRepository.UpdateGroup(oldName, newName);
        }

        public async Task<int?> UpdatePriority(PriorityDTO priorityEntity)
        {
            var priority = mapper.Map<PriorityEntityDTO>(priorityEntity);
            return await configurationRepository.UpdatePriority(priority);
        }

        public async Task<int?> UpdateStatus(StatusDTO statusEntity)
        {
            var status = mapper.Map<StatusEntityDTO>(statusEntity);
            return await configurationRepository.UpdateStatus(status);
        }

        public async Task<int?> UpdateProjectStatus(ProjectStatusDTO projectStatusEntity)
        {
            var projectStatus = mapper.Map<ProjectStatusEntityDTO>(projectStatusEntity);
            return await configurationRepository.UpdateProjectStatus(projectStatus);
        }

        public async Task<int?> UpdateType(TypeDTO typeEntity)
        {
            var type = mapper.Map<TypeEntityDTO>(typeEntity);
            return await configurationRepository.UpdateType(type);
        }

        public async Task<int?> UpdateLabel(LabelDTO labelEntity)
        {
            var label = mapper.Map<LabelEntityDTO>(labelEntity);
            return await configurationRepository.UpdateLabel(label);
        }

        public List<string> GetGroups()
        {
            return configurationRepository.GetGroups();
        }

        public List<PriorityDTO> GetPriorities()
        {
            var priorities = configurationRepository.GetPriorities();
            return mapper.Map<List<PriorityDTO>>(priorities);
        }

        public List<StatusDTO> GetStatusByTypeId(int typeId)
        {
            var statuses = configurationRepository.GetStatusByTypeId(typeId);
            return mapper.Map<List<StatusDTO>>(statuses);
        }

        public List<ProjectStatusDTO> GetProjectStatuses()
        {
            var projectStatuses = configurationRepository.GetProjectStatuses();
            return mapper.Map<List<ProjectStatusDTO>>(projectStatuses);
        }

        public List<TypeDTO> GetTypes()
        {
            var types = configurationRepository.GetTypes();
            return mapper.Map<List<TypeDTO>>(types);
        }

        public List<LabelDTO> GetLabels()
        {
            var labels = configurationRepository.GetLabels();
            return mapper.Map<List<LabelDTO>>(labels);
        }

        public async Task<bool> DeleteGroup(string name)
        {
            return await configurationRepository.DeleteGroup(name);
        }

        public async Task<bool> DeletePriority(int id)
        {
            return await configurationRepository.DeletePriority(id);
        }

        public async Task<bool> DeleteStatus(int id)
        {
            return await configurationRepository.DeleteStatus(id);
        }

        public async Task<bool> DeleteProjectStatus(int id)
        {
            return await configurationRepository.DeleteProjectStatus(id);
        }

        public async Task<bool> DeleteType(int id)
        {
            return await configurationRepository.DeleteType(id);
        }

        public async Task<bool> DeleteLabel(int id)
        {
            return await configurationRepository.DeleteLabel(id);
        }
    }
}
