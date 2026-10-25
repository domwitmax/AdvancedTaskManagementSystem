using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.AssignmentTask;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.AddTaskAttachment;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.ChangeTaskStatus;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.CreateTask;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.EditTask;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.FilterListTasks;
using Microsoft.AspNetCore.Http;
using AdvancedTaskManagementSystemCQRS.WebApi.Request;
using AutoMapper;

namespace AdvancedTaskManagementSystemCQRS.WebApi.Tasks
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<CreateTaskRequest, CreateTaskCommand>();
            CreateMap<EditTaskRequest, EditTaskCommand>();
            CreateMap<AssignmentTaskRequest, AssignmentTaskCommand>();
            CreateMap<ChangeTaskStatusRequest, ChangeTaskStatusCommand>();
            CreateMap<FilterListTasksRequest, FilterListTasksQuery>();
            CreateMap<IFormFile, TaskAttachmentDTO>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TaskId, opt => opt.Ignore())
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.ContentType, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.ContentType) ? "application/octet-stream" : src.ContentType))
                .ForMember(dest => dest.FileSize, opt => opt.MapFrom(src => src.Length))
                .ForMember(dest => dest.Content, opt => opt.Ignore())
                .ForMember(dest => dest.UploadedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UploadedAt, opt => opt.Ignore());
            CreateMap<TaskAttachmentDTO, AddTaskAttachmentCommand>();
        }
    }
}
