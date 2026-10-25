using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.DTOs;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Project.GetProjectEntityById;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Project.GetProjectEntityById
{
    public class GetProjectEntityByIdQueryHandler(TaskManagerDbContext dbContext, IMapper mapper) : IRequestHandler<GetProjectEntityByIdQuery, ProjectEntityDTO?>
    {
        public async Task<ProjectEntityDTO?> Handle(GetProjectEntityByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await dbContext.Projects
                .Include(item => item.Status)
                .FirstOrDefaultAsync(item => item.Id == request.ProjectId, cancellationToken);

            return mapper.Map<ProjectEntityDTO>(project);
        }
    }
}
