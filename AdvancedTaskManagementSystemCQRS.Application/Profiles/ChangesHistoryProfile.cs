using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;
using CommonChangeDetailDTO = AdvancedTaskManagementSystemCQRS.Application.Common.ChangesHistory.DTOs.ChangeDetailDTO;
using CommonChangesHistoryDTO = AdvancedTaskManagementSystemCQRS.Application.Common.ChangesHistory.DTOs.ChangesHistoryDTO;
using RepositoryChangeDetailDTO = AdvancedTaskManagementSystemCQRS.Application.Repository.ChangesHistory.DTOs.ChangeDetailEntityDTO;
using RepositoryChangesHistoryDTO = AdvancedTaskManagementSystemCQRS.Application.Repository.ChangesHistory.DTOs.ChangesHistoryEntityDTO;

namespace AdvancedTaskManagementSystemCQRS.Application.Profiles;

public class ChangesHistoryProfile : Profile
{
    public ChangesHistoryProfile()
    {
        CreateMap<ChangeDetailEntity, RepositoryChangeDetailDTO>();
        CreateMap<ChangesHistoryEntity, RepositoryChangesHistoryDTO>();

        CreateMap<RepositoryChangeDetailDTO, CommonChangeDetailDTO>();
        CreateMap<RepositoryChangesHistoryDTO, CommonChangesHistoryDTO>();
    }
}
