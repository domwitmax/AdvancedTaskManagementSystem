using AdvancedTaskManagementSystemCQRS.Application.Common.Account.ChangePassword;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.ChangeRole;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.Login;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.Register;
using AdvancedTaskManagementSystemCQRS.Application.Common.Account.UpdateAccount;
using AdvancedTaskManagementSystemCQRS.WebApi.Account.Request;
using AdvancedTaskManagementSystemCQRS.WebApi.Account.Response;
using AutoMapper;

namespace AdvancedTaskManagementSystemCQRS.WebApi.Account;

public class AccountProfileWebApi : Profile
{
    public AccountProfileWebApi()
    {
        CreateMap<LoginRequest, LoginCommand>();
        CreateMap<RegisterRequest, RegisterCommand>();
        CreateMap<ChangePasswordRequest, ChangePasswordCommand>();
        CreateMap<UpdateAccountRequest, UpdateAccountCommonCommand>();
        CreateMap<ChangeRoleRequest, ChangeRoleCommand>();
        CreateMap<GetAccountCommonDTO, GetAccountResponse>();
        CreateMap<GetActualAccountCommonDTO, GetActualAccountResponse>();
    }
}
