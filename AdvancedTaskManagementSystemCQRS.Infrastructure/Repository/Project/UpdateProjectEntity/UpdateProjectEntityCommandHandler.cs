using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.UpdateProjectEntity;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Project.UpdateProjectEntity
{
    public class UpdateProjectEntityCommandHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateProjectEntityCommand, int?>
    {
        public async Task<int?> Handle(UpdateProjectEntityCommand request, CancellationToken cancellationToken)
        {
            var project = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (project == null) return null;

            mapper.Map(request, project);

            await dbContext.SaveChangesAsync(cancellationToken);
            return project.Id;
        }
    }
}
