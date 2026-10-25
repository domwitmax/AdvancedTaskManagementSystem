using AdvancedTaskManagementSystemCQRS.Application.Common.Account.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.Register;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.UpdateAccount;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.AddAccount;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.UpdateAccount;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;

namespace AdvancedTaskManagementSystemCQRS.Application.Profiles;

public class AccountProfileApplication : Profile
{
    public AccountProfileApplication()
    {
        CreateMap<AddAccountEntityCommand, AccountEntity>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.ChangePasswordAt, opt => opt.Ignore())
            .ForMember(dest => dest.LastLoginAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.LastGetUserAt, opt => opt.Ignore())
            .ForMember(dest => dest.Active, opt => opt.Ignore())
            .ForMember(dest => dest.RoleId, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectMemberships, opt => opt.Ignore());

        CreateMap<UpdateAccountEntityCommand, AccountEntity>()
            .ForMember(dest => dest.Login, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.LastLoginAt, opt => opt.Ignore())
            .ForMember(dest => dest.LastGetUserAt, opt => opt.Ignore())
            .ForMember(dest => dest.RoleId, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.ProjectMemberships, opt => opt.Ignore())
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<AccountEntity, GetAccountEntityDTO>()
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
        CreateMap<GetAccountEntityDTO, GetActualAccountCommonDTO>();
        CreateMap<GetAccountEntityDTO, GetAccountCommonDTO>();

        CreateMap<RegisterCommand, AddAccountEntityCommand>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Active, opt => opt.Ignore());

        CreateMap<GetAccountEntityDTO, UpdateAccountEntityCommand>();
        CreateMap<UpdateAccountCommonCommand, UpdateAccountEntityCommand>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.ChangePasswordAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.Active, opt => opt.Ignore());

        CreateMap<RoleEntity, RoleEntityDTO>();
        CreateMap<RoleEntityDTO, RoleDTO>();
    }
}
