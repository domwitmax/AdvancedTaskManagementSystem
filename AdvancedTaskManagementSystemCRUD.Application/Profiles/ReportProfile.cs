using AdvancedTaskManagementSystemCRUD.Application.Services.Reports.DTOs;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.Application.Profiles;

public class ReportProfile : Profile
{
    public ReportProfile()
    {
        CreateMap<(string Status, int Count), TasksByStatusDTO>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count));

        CreateMap<(int ProjectId, int AllTasks, int CompletedTasks), ProjectProgressDTO>()
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
            .ForMember(dest => dest.AllTasks, opt => opt.MapFrom(src => src.AllTasks))
            .ForMember(dest => dest.CompletedTasks, opt => opt.MapFrom(src => src.CompletedTasks));

        CreateMap<(string AssignedTo, int TasksCount), UserWorkloadDTO>()
            .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.AssignedTo))
            .ForMember(dest => dest.TasksCount, opt => opt.MapFrom(src => src.TasksCount));
    }
}
