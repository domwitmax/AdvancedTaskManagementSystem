using AdvancedTaskManagementSystemCQRS.Application.Common.Comments.AddComment;
using MediatR;
using Moq;
using RepositoryAddCommentCommand = AdvancedTaskManagementSystemCQRS.Application.Repository.Comments.AddComment.AddCommentCommand;

namespace AdvancedTaskManagementSystemCQRS.Tests.Handlers;

public class AddCommentCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldForwardCommandToPersistenceLayerAndReturnIdentifier()
    {
        var mediator = new Mock<IMediator>();
        mediator
            .Setup(item => item.Send(
                It.Is<RepositoryAddCommentCommand>(command =>
                    command.TaskId == 12 &&
                    command.Content == "Komentarz testowy" &&
                    command.CreatedBy == "student"),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(25);

        var handler = new AddCommentCommandHandler(mediator.Object);
        var request = new AddCommentCommand(12, "Komentarz testowy", "student");

        var result = await handler.Handle(request, CancellationToken.None);

        Assert.Equal(25, result);
        mediator.Verify(item => item.Send(
            It.Is<RepositoryAddCommentCommand>(command =>
                command.TaskId == request.TaskId &&
                command.Content == request.Content &&
                command.CreatedBy == request.CreatedBy),
            It.IsAny<CancellationToken>()), Times.Once);
    }
}
