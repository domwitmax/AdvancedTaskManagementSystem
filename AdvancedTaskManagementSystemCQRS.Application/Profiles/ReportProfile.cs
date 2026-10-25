using AdvancedTaskManagementSystemCQRS.Application.Common.Reports.DTOs;
using AutoMapper;

namespace AdvancedTaskManagementSystemCQRS.Application.Profiles;

public class ReportProfile : Profile
{
    public ReportProfile()
    {
        CreateMap<(string Status, int Count), TasksByStatusDTO>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count));

        CreateMap<(string AssignedTo, int TasksCount), UserWorkloadDTO>()
            .ForMember(dest => dest.AssignedTo, opt => opt.MapFrom(src => src.AssignedTo))
            .ForMember(dest => dest.TasksCount, opt => opt.MapFrom(src => src.TasksCount));

        CreateMap<(int ProjectId, int AllTasks, int CompletedTasks), ProjectProgressDTO>()
            .ConstructUsing(src => new ProjectProgressDTO(src.ProjectId, src.AllTasks, src.CompletedTasks));
    }
}
