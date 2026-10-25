using AdvancedTaskManagementSystemCRUD.Application.Services.Account.DTOs;
using AdvancedTaskManagementSystemCRUD.Domain.Entities;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.Application.Profiles;

public class AccountProfileApplication : Profile
{
    public AccountProfileApplication()
    {
        CreateMap<AccountEntity, GetActualAccountDTO>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
        CreateMap<AccountEntity, GetAccountDTO>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
        CreateMap<RoleEntity, RoleDTO>();
        CreateMap<RegisterDTO, AccountEntity>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.LastLoginAt, opt => opt.Ignore())
            .ForMember(dest => dest.LastGetUserAt, opt => opt.Ignore())
            .ForMember(dest => dest.ChangePasswordAt, opt => opt.Ignore())
            .ForMember(dest => dest.Active, opt => opt.Ignore())
            .ForMember(dest => dest.RoleId, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectMemberships, opt => opt.Ignore());
        CreateMap<(UpdateAccountDTO, AccountEntity), AccountEntity>()
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.Item1.Login))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Item1.Email))
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Item2.PasswordHash))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.Item2.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.LastLoginAt, opt => opt.MapFrom(src => src.Item2.LastLoginAt))
            .ForMember(dest => dest.LastGetUserAt, opt => opt.MapFrom(src => src.Item2.LastGetUserAt))
            .ForMember(dest => dest.ChangePasswordAt, opt => opt.MapFrom(src => src.Item2.ChangePasswordAt))
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Item2.Active))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.Item2.RoleId))
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectMemberships, opt => opt.Ignore());
    }
}
