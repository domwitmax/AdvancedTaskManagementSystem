using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.AddLabelEntity;
using AdvancedTaskManagementSystemCQRS.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.AddLabelEntity;

public class AddLabelEntityCommandHandler(TaskManagerDbContext dbContext, IMapper mapper)
    : IRequestHandler<AddLabelEntityCommand, int>
{
    public async Task<int> Handle(AddLabelEntityCommand request, CancellationToken cancellationToken)
    {
        var normalizedName = request.Name.Trim();
        if (await dbContext.Labels.AnyAsync(
                item => item.Name.ToLower() == normalizedName.ToLower(),
                cancellationToken))
            return 0;

        var label = mapper.Map<LabelEntity>(request);
        label.Name = normalizedName;
        dbContext.Labels.Add(label);
        await dbContext.SaveChangesAsync(cancellationToken);
        return label.Id;
    }
}
