using AdvancedTaskManagementSystemCRUD.WebApi.Account.Response;
using AdvancedTaskManagementSystemCRUD.Application.Services.Account.DTOs;
using AdvancedTaskManagementSystemCRUD.WebApi.Account.Request;
using AutoMapper;

namespace AdvancedTaskManagementSystemCRUD.WebApi.Account;

public class AccountProfileWebApi : Profile
{
    public AccountProfileWebApi()
    {
        CreateMap<LoginRequest, LoginDTO>();
        CreateMap<RegisterRequest, RegisterDTO>();
        CreateMap<ChangePasswordRequest, ChangePasswordDTO>();
        CreateMap<UpdateAccountRequest, UpdateAccountDTO>();
        CreateMap<ChangeRoleRequest, ChangeRoleDTO>();
        CreateMap<GetAccountDTO, GetAccountResponse>();
        CreateMap<GetActualAccountDTO, GetActualAccountResponse>();
    }
}
