using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Services.Comment;
using AdvancedTaskManagementSystemCRUD.Application.Services.Comments.DTOs;
using AutoMapper;
using Moq;
using RepositoryCommentDTO = AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs.CommentDTO;

namespace AdvancedTaskManagementSystemCRUD.Tests.Services;

public class CommentServiceTests
{
    [Fact]
    public async Task AddComment_ShouldMapDtoAndReturnRepositoryIdentifier()
    {
        var repository = new Mock<ICommentRepository>();
        var mapper = new Mock<IMapper>();
        var serviceDto = new CommentDTO
        {
            TaskId = 12,
            Content = "Komentarz testowy",
            CreatedBy = "student"
        };
        var repositoryDto = new RepositoryCommentDTO
        {
            TaskId = 12,
            Content = "Komentarz testowy",
            CreatedBy = "student"
        };

        mapper
            .Setup(item => item.Map<RepositoryCommentDTO>(serviceDto))
            .Returns(repositoryDto);
        repository
            .Setup(item => item.AddComment(repositoryDto))
            .ReturnsAsync(25);

        var service = new CommentService(repository.Object, mapper.Object);

        var result = await service.AddComment(serviceDto);

        Assert.Equal(25, result);
        mapper.Verify(item => item.Map<RepositoryCommentDTO>(serviceDto), Times.Once);
        repository.Verify(item => item.AddComment(repositoryDto), Times.Once);
    }
}
