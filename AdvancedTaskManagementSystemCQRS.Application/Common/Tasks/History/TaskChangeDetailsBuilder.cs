using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.CreateTask;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.EditTask;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;

namespace AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.History;

public static class TaskChangeDetailsBuilder
{
    public static List<TaskChangeDetailDTO> ForCreate(
        CreateTaskCommand request,
        string typeName,
        string statusName,
        string projectName,
        string priorityName,
        IEnumerable<string> labelNames)
    {
        var details = new List<TaskChangeDetailDTO>
        {
            CreateDetail("Task", string.Empty, "Utworzono zadanie"),
            CreateDetail("Title", string.Empty, request.Title),
            CreateDetail("Description", string.Empty, request.Description),
            CreateDetail("Type", string.Empty, typeName),
            CreateDetail("Status", string.Empty, statusName),
            CreateDetail("Project", string.Empty, projectName),
            CreateDetail("Priority", string.Empty, priorityName),
            CreateDetail("DueDate", string.Empty, FormatDate(request.DueDate)),
            CreateDetail("AssignedTo", string.Empty, request.AssignedTo),
            CreateDetail("Labels", string.Empty, string.Join(", ", labelNames.OrderBy(item => item)))
        };

        return details
            .Where(item => !string.IsNullOrEmpty(item.NewValue))
            .ToList();
    }

    public static List<TaskChangeDetailDTO> ForEdit(
        TaskEntityDTO current,
        EditTaskCommand request,
        string newTypeName,
        string newStatusName,
        string newProjectName,
        string newPriorityName,
        IEnumerable<string> newLabelNames)
    {
        var details = new List<TaskChangeDetailDTO>();

        AddDetail(details, "Title", current.Title, request.Title);
        AddDetail(details, "Description", current.Description, request.Description);
        AddDetail(details, "DueDate", FormatDate(current.DueDate), FormatDate(request.DueDate));
        AddDetail(details, "AssignedTo", current.AssignedTo, request.AssignedTo, true);

        if (current.TypeId != request.TypeId)
            AddDetail(details, "Type", current.Type, newTypeName);

        if (current.StatusId != request.StatusId)
            AddDetail(details, "Status", current.Status, newStatusName);

        if (current.ProjectId != request.ProjectId)
            AddDetail(details, "Project", current.ProjectName, newProjectName);

        if (current.PriorityId != request.PriorityId)
            AddDetail(details, "Priority", current.Priority, newPriorityName);

        var oldLabelIds = current.LabelIds.Distinct().OrderBy(item => item).ToList();
        var newLabelIds = request.LabelIds?.Distinct().OrderBy(item => item).ToList() ?? new List<int>();
        if (!oldLabelIds.SequenceEqual(newLabelIds))
        {
            AddDetail(
                details,
                "Labels",
                string.Join(", ", current.Labels.OrderBy(item => item)),
                string.Join(", ", newLabelNames.OrderBy(item => item)));
        }

        return details;
    }

    public static List<TaskChangeDetailDTO> ForAssignment(string? oldValue, string? newValue)
    {
        var details = new List<TaskChangeDetailDTO>();
        AddDetail(details, "AssignedTo", oldValue, newValue, true);
        return details;
    }

    public static List<TaskChangeDetailDTO> ForStatus(string? oldValue, string? newValue)
    {
        var details = new List<TaskChangeDetailDTO>();
        AddDetail(details, "Status", oldValue, newValue);
        return details;
    }

    public static List<TaskChangeDetailDTO> ForAttachment(string fileName)
        => new()
        {
            CreateDetail("Attachment", string.Empty, fileName)
        };

    private static void AddDetail(
        ICollection<TaskChangeDetailDTO> details,
        string propertyName,
        string? oldValue,
        string? newValue,
        bool ignoreCase = false)
    {
        var oldValueToCompare = oldValue ?? string.Empty;
        var newValueToCompare = newValue ?? string.Empty;
        var comparison = ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

        if (string.Equals(oldValueToCompare, newValueToCompare, comparison))
            return;

        details.Add(CreateDetail(propertyName, oldValueToCompare, newValueToCompare));
    }

    private static TaskChangeDetailDTO CreateDetail(string propertyName, string? oldValue, string? newValue)
        => new()
        {
            PropertyName = FormatValue(propertyName),
            OldValue = FormatValue(oldValue),
            NewValue = FormatValue(newValue)
        };

    private static string FormatDate(DateTime? value)
        => value?.ToString("yyyy-MM-dd HH:mm:ss") ?? string.Empty;

    private static string FormatValue(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        return value.Length <= 100 ? value : value[..97] + "...";
    }
}
