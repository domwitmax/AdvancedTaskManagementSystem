using AdvancedTaskManagementSystemCQRS.Application.Common.Comments.CanModifyComment;

namespace AdvancedTaskManagementSystemCQRS.Tests.Authorization;

public class CanModifyCommentQueryHandlerTests
{
    [Theory]
    [InlineData("author", "author", false, true)]
    [InlineData("Author", "author", false, true)]
    [InlineData("author", "other", false, false)]
    [InlineData("author", null, false, false)]
    [InlineData("author", "admin", true, true)]
    public async Task Handle_ShouldApplyCommentOwnershipPolicy(
        string createdBy,
        string? currentUser,
        bool isAdministrator,
        bool expected)
    {
        var handler = new CanModifyCommentQueryHandler();

        var result = await handler.Handle(
            new CanModifyCommentQuery(createdBy, currentUser, isAdministrator),
            CancellationToken.None);

        Assert.Equal(expected, result);
    }
}
