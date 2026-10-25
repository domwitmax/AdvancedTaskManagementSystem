using AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;
using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.Application.Profiles
{
    public class ChangesHistoryProfile : Profile
    {
        public ChangesHistoryProfile()
        {
            CreateMap<ChangeDetailEntityDTO, ChangeDetailEntity>();
            CreateMap<ChangeDetailEntity, ChangeDetailEntityDTO>();
            CreateMap<ChangesHistoryEntityDTO, ChangesHistoryEntity>();
            CreateMap<ChangesHistoryEntity, ChangesHistoryEntityDTO>();
        }
    }
}
