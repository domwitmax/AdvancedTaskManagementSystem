using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetAccounts;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Account.GetAccounts;

public class GetAccountEntitiesQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetAccountEntitiesQuery, List<GetAccountEntityDTO>>
{
    public async Task<List<GetAccountEntityDTO>> Handle(GetAccountEntitiesQuery request, CancellationToken cancellationToken)
    {
        var accounts = await dbContext.Account
            .AsNoTracking()
            .Include(item => item.Role)
            .OrderBy(item => item.Login)
            .ToListAsync(cancellationToken);
        return mapper.Map<List<GetAccountEntityDTO>>(accounts);
    }
}
