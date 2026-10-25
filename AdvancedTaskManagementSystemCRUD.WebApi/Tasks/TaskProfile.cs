using AdvancedTaskManagementSystemCRUD.Application.Services.Task.DTOs;
using AdvancedTaskManagementSystemCRUD.WebApi.Request;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace AdvancedTaskManagementSystemCRUD.WebApi.Tasks
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<CreateTaskRequest, TaskDTO>();
            CreateMap<EditTaskRequest, TaskDTO>();
            CreateMap<AssignmentTaskRequest, AssignmentTaskDTO>();
            CreateMap<ChangeTaskStatusRequest, ChangeTaskStatusDTO>();
            CreateMap<FilterListTasksRequest, FilterListTasksDTO>();
            CreateMap<IFormFile, TaskAttachmentDTO>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TaskId, opt => opt.Ignore())
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.ContentType) ? "application/octet-stream" : src.ContentType))
                .ForMember(dest => dest.FileSize, opt => opt.MapFrom(src => src.Length))
                .ForMember(dest => dest.Content, opt => opt.Ignore())
                .ForMember(dest => dest.UploadedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UploadedAt, opt => opt.Ignore());
        }
    }
}
