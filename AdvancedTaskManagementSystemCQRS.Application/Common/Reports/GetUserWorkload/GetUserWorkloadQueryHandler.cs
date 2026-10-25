using AdvancedTaskManagementSystemCQRS.Application.Common.Reports.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Account.GetAccounts;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.GetTaskEntities;
using AutoMapper;
using MediatR;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Reports.GetUserWorkload;

public class GetUserWorkloadQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetUserWorkloadQuery, List<UserWorkloadDTO>>
{
    public async Task<List<UserWorkloadDTO>> Handle(GetUserWorkloadQuery request, CancellationToken cancellationToken)
    {
        var tasks = await mediator.Send(new GetTaskEntitiesQuery(), cancellationToken);
        var accounts = await mediator.Send(new GetAccountEntitiesQuery(), cancellationToken);
        var workloads = accounts
            .Where(account => account.Active)
            .OrderBy(account => account.Login)
            .Select(account => (
                AssignedTo: account.Login,
                TasksCount: tasks.Count(task => task.AssignedTo.Equals(account.Login, StringComparison.OrdinalIgnoreCase))))
            .ToList();

        return mapper.Map<List<UserWorkloadDTO>>(workloads);
    }
}
