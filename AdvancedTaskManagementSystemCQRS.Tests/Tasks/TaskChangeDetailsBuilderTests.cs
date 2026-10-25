using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.CreateTask;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.EditTask;
using AdvancedTaskManagementSystemCQRS.Application.Common.Tasks.History;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Tasks.DTOs;

namespace AdvancedTaskManagementSystemCQRS.Tests.Tasks;

public class TaskChangeDetailsBuilderTests
{
    [Fact]
    public void ForCreate_ShouldCreateHistoryForTaskDataAndSortLabels()
    {
        var request = new CreateTaskCommand
        {
            Title = "Naprawa logowania",
            Description = "Poprawa walidacji logowania",
            TypeId = 1,
            StatusId = 2,
            ProjectId = 3,
            PriorityId = 4,
            AssignedTo = "student",
            CreatedBy = "admin",
            LabelIds = new List<int> { 2, 1 }
        };

        var result = TaskChangeDetailsBuilder.ForCreate(
            request,
            "Bug",
            "Nowy",
            "System zadań",
            "Wysoki",
            new[] { "pilne", "bug" });

        Assert.Contains(result, detail =>
            detail.PropertyName == "Task" && detail.NewValue == "Utworzono zadanie");
        Assert.Contains(result, detail =>
            detail.PropertyName == "Title" && detail.NewValue == "Naprawa logowania");
        Assert.Contains(result, detail =>
            detail.PropertyName == "Labels" && detail.NewValue == "bug, pilne");
        Assert.DoesNotContain(result, detail => detail.PropertyName == "DueDate");
    }

    [Fact]
    public void ForAssignment_WhenValueDiffersOnlyByCase_ShouldNotCreateHistoryEntry()
    {
        var result = TaskChangeDetailsBuilder.ForAssignment("Student", "student");

        Assert.Empty(result);
    }

    [Fact]
    public void ForStatus_WhenStatusChanges_ShouldCreateSingleHistoryEntry()
    {
        var result = TaskChangeDetailsBuilder.ForStatus("Nowy", "W trakcie");

        var detail = Assert.Single(result);
        Assert.Equal("Status", detail.PropertyName);
        Assert.Equal("Nowy", detail.OldValue);
        Assert.Equal("W trakcie", detail.NewValue);
    }

    [Fact]
    public void ForEdit_ShouldIncludeOnlyChangedProperties()
    {
        var current = new TaskEntityDTO
        {
            Id = 10,
            Title = "Stary tytuł",
            Description = "Opis",
            TypeId = 1,
            Type = "Bug",
            StatusId = 2,
            Status = "Nowy",
            ProjectId = 3,
            ProjectName = "System zadań",
            PriorityId = 4,
            Priority = "Wysoki",
            AssignedTo = "student",
            LabelIds = new List<int> { 1, 2 },
            Labels = new List<string> { "bug", "pilne" }
        };

        var request = new EditTaskCommand
        {
            Id = 10,
            Title = "Nowy tytuł",
            Description = "Opis",
            TypeId = 1,
            StatusId = 2,
            ProjectId = 3,
            PriorityId = 4,
            AssignedTo = "STUDENT",
            ChangedBy = "admin",
            LabelIds = new List<int> { 1, 3 }
        };

        var result = TaskChangeDetailsBuilder.ForEdit(
            current,
            request,
            "Bug",
            "Nowy",
            "System zadań",
            "Wysoki",
            new[] { "bug", "feature" });

        Assert.Equal(2, result.Count);
        Assert.Contains(result, detail =>
            detail.PropertyName == "Title" &&
            detail.OldValue == "Stary tytuł" &&
            detail.NewValue == "Nowy tytuł");
        Assert.Contains(result, detail =>
            detail.PropertyName == "Labels" &&
            detail.NewValue == "bug, feature");
        Assert.DoesNotContain(result, detail => detail.PropertyName == "AssignedTo");
    }

    [Fact]
    public void ForAttachment_ShouldCreateAttachmentHistoryEntry()
    {
        var result = TaskChangeDetailsBuilder.ForAttachment("specyfikacja.pdf");

        var detail = Assert.Single(result);
        Assert.Equal("Attachment", detail.PropertyName);
        Assert.Equal(string.Empty, detail.OldValue);
        Assert.Equal("specyfikacja.pdf", detail.NewValue);
    }
}
