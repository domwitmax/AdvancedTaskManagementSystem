using AdvancedTaskManagementSystemCQRS.Application.Repository.Configurations.DeleteLabelEntity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AdvancedTaskManagementSystemCQRS.Infrastructure.Repository.Configurations.DeleteLabelEntity;

public class DeleteLabelEntityCommandHandler(TaskManagerDbContext taskManagerDbContext) : IRequestHandler<DeleteLabelEntityCommand, bool>
{
    public async Task<bool> Handle(DeleteLabelEntityCommand request, CancellationToken cancellationToken)
    {
        var label = await taskManagerDbContext.Labels.FirstOrDefaultAsync(l => l.Id == request.Id, cancellationToken);
        if (label == null) return false;

        var hasTaskLabels = await taskManagerDbContext.TaskLabels.AnyAsync(tl => tl.LabelId == request.Id, cancellationToken);
        if (hasTaskLabels) return false;

        taskManagerDbContext.Labels.Remove(label);
        await taskManagerDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
