using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.UpdateAccount;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Account.UpdateAccount;

public class UpdateAccountEntityCommandHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateAccountEntityCommand>
{
    public async Task Handle(UpdateAccountEntityCommand request, CancellationToken cancellationToken)
    {
        var userFromDb = await dbContext.Account.SingleAsync(item => item.Login == request.Login, cancellationToken);
        mapper.Map(request, userFromDb);
        userFromDb.UpdatedAt = DateTime.UtcNow;
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
