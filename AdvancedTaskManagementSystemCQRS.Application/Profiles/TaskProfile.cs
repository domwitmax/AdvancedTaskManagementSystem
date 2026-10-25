using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.CreateTask;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.EditTask;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.AddTaskAttachment;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.CreateTaskEntity;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.AddTaskAttachmentEntity;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.UpdateTaskEntity;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;

namespace AdvancedTaskManagementSystemCQRS.Application.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<CreateTaskEntityCommand, TaskEntity>()
                .ForMember(dest => dest.Creator, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ForMember(dest => dest.Priority, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.TaskLabels, opt => opt.Ignore())
                .ForMember(dest => dest.Attachments, opt => opt.Ignore());
            CreateMap<TaskEntity, TaskEntityDTO>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.Name))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name))
                .ForMember(dest => dest.LabelIds, opt => opt.MapFrom(src => src.TaskLabels.Select(label => label.LabelId).ToList()))
                .ForMember(dest => dest.Labels, opt => opt.MapFrom(src => src.TaskLabels.Select(label => label.Label.Name).ToList()));
            CreateMap<UpdateTaskEntityCommand, TaskEntity>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.Creator, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.Type, opt => opt.Ignore())
                .ForMember(dest => dest.Priority, opt => opt.Ignore())
                .ForMember(dest => dest.Project, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.TaskLabels, opt => opt.Ignore())
                .ForMember(dest => dest.Attachments, opt => opt.Ignore());

            CreateMap<CreateTaskCommand, CreateTaskEntityCommand>();
            CreateMap<TaskEntity, UpdateTaskEntityCommand>();
            CreateMap<EditTaskCommand, TaskEntityDTO>();
            CreateMap<TaskEntityDTO, UpdateTaskEntityCommand>();

            CreateMap<TaskEntityDTO, GetTaskDTO>();

            CreateMap<AddTaskAttachmentCommand, AddTaskAttachmentEntityCommand>();
            CreateMap<AddTaskAttachmentEntityCommand, TaskAttachmentEntity>()
                .ForMember(dest => dest.UploadedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.Task, opt => opt.Ignore());
            CreateMap<TaskAttachmentEntity, TaskAttachmentEntityDTO>();
            CreateMap<TaskAttachmentEntityDTO, TaskAttachmentDTO>();
        }
    }
}
