using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.UpdateLabelEntity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.UpdateLabelEntity;

public class UpdateLabelEntityCommandHandler(TaskManagerDbContext dbContext)
    : IRequestHandler<UpdateLabelEntityCommand, int?>
{
    public async Task<int?> Handle(UpdateLabelEntityCommand request, CancellationToken cancellationToken)
    {
        var label = await dbContext.Labels.FirstOrDefaultAsync(item => item.Id == request.Id, cancellationToken);
        if (label == null)
            return null;

        var normalizedName = request.Name.Trim();
        if (await dbContext.Labels.AnyAsync(
                item => item.Id != request.Id && item.Name.ToLower() == normalizedName.ToLower(),
                cancellationToken))
            return 0;

        label.Name = normalizedName;
        label.Description = request.Description;
        await dbContext.SaveChangesAsync(cancellationToken);
        return label.Id;
    }
}
