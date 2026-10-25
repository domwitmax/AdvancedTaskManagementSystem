using AdvancedTaskManagementSystemCRUD.Application.Services.Task.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Repository.Tasks.DTOs;
using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.Application.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<TaskDTO, TaskEntityDTO>()
                .ForMember(dest => dest.ChangedBy, opt => opt.Ignore());
            CreateMap<TaskEntityDTO, TaskDTO>();

            CreateMap<TaskEntityDTO, TaskEntity>()
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
                .ForMember(dest => dest.StatusIsFinished, opt => opt.MapFrom(src => src.Status.IsFinished))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Project.Name))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority.Name))
                .ForMember(dest => dest.LabelIds, opt => opt.MapFrom(src => src.TaskLabels.Select(label => label.LabelId).ToList()))
                .ForMember(dest => dest.Labels, opt => opt.MapFrom(src => src.TaskLabels.Select(label => label.Label.Name).ToList()))
                .ForMember(dest => dest.ChangedBy, opt => opt.Ignore());

            CreateMap<TaskAttachmentDTO, TaskAttachmentEntityDTO>();
            CreateMap<TaskAttachmentEntityDTO, TaskAttachmentDTO>();
            CreateMap<TaskAttachmentEntityDTO, TaskAttachmentEntity>();
            CreateMap<TaskAttachmentEntity, TaskAttachmentEntityDTO>();
        }
    }
}
