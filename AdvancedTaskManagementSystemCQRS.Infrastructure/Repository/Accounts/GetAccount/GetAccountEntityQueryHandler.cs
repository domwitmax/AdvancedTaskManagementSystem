using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.DTOs;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetAccount;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Account.GetAccount;

public class GetAccountEntityQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetAccountEntityQuery, GetAccountEntityDTO?>
{
    public async Task<GetAccountEntityDTO?> Handle(GetAccountEntityQuery request, CancellationToken cancellationToken)
    {
        var account = await dbContext.Account
            .AsNoTracking()
            .Include(item => item.Role)
            .FirstOrDefaultAsync(item => item.Login == request.UserName, cancellationToken);
        return mapper.Map<GetAccountEntityDTO?>(account);
    }
}
