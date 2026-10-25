using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.CreateProjectEntity;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Project.CreateProjectEntity;

public class CreateProjectEntityQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<CreateProjectEntityCommand, int>
{
    public async Task<int> Handle(CreateProjectEntityCommand request, CancellationToken cancellationToken)
    {
        if (!await dbContext.Account.AnyAsync(
                item => item.Login == request.CreatedBy && item.Active,
                cancellationToken))
            return 0;

        var project = mapper.Map<ProjectEntity>(request);
        project.ProjectMembers.Add(new ProjectMemberEntity
        {
            AccountLogin = request.CreatedBy,
            AddedAt = DateTime.UtcNow,
            AddedBy = request.CreatedBy
        });

        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync(cancellationToken);
        return project.Id;
    }
}
