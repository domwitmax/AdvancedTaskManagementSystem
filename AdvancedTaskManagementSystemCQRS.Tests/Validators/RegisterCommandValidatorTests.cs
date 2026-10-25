using AdvancedTaskManagementSystemCQRS.Application.Common.Account.Register;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Accounts.IsEmailTaken;
using AdvancedTaskManagementSystemCQRS.Application.Repository.Accounts.IsLoginTaken;
using MediatR;
using Moq;

namespace AdvancedTaskManagementSystemCQRS.Tests.Validators;

public class RegisterCommandValidatorTests
{
    private readonly Mock<IMediator> _mediator = new();

    [Fact]
    public async Task ValidateAsync_WhenDataIsCorrect_ShouldBeValid()
    {
        _mediator
            .Setup(mediator => mediator.Send(It.IsAny<IsLoginTakenQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _mediator
            .Setup(mediator => mediator.Send(It.IsAny<IsEmailTakenQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var validator = new RegisterCommandValidator(_mediator.Object);
        var request = new RegisterCommand("student", "student@example.com", "Password123");

        var result = await validator.ValidateAsync(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public async Task ValidateAsync_WhenLoginIsTaken_ShouldReturnValidationError()
    {
        _mediator
            .Setup(mediator => mediator.Send(It.IsAny<IsLoginTakenQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
        _mediator
            .Setup(mediator => mediator.Send(It.IsAny<IsEmailTakenQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var validator = new RegisterCommandValidator(_mediator.Object);
        var request = new RegisterCommand("student", "student@example.com", "Password123");

        var result = await validator.ValidateAsync(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
            error.PropertyName == nameof(RegisterCommand.Login) &&
            error.ErrorMessage == "Login jest już zajęty.");
    }

    [Fact]
    public async Task ValidateAsync_WhenEmailIsTaken_ShouldReturnValidationError()
    {
        _mediator
            .Setup(mediator => mediator.Send(It.IsAny<IsLoginTakenQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _mediator
            .Setup(mediator => mediator.Send(It.IsAny<IsEmailTakenQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var validator = new RegisterCommandValidator(_mediator.Object);
        var request = new RegisterCommand("student", "student@example.com", "Password123");

        var result = await validator.ValidateAsync(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
            error.PropertyName == nameof(RegisterCommand.Email) &&
            error.ErrorMessage == "Email jest już zajęty.");
    }

    [Fact]
    public async Task ValidateAsync_WhenPasswordIsTooShort_ShouldReturnValidationError()
    {
        _mediator
            .Setup(mediator => mediator.Send(It.IsAny<IsLoginTakenQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _mediator
            .Setup(mediator => mediator.Send(It.IsAny<IsEmailTakenQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var validator = new RegisterCommandValidator(_mediator.Object);
        var request = new RegisterCommand("student", "student@example.com", "12345");

        var result = await validator.ValidateAsync(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
            error.PropertyName == nameof(RegisterCommand.Password) &&
            error.ErrorMessage == "Hasło musi mieć co najmniej 6 znaków.");
    }
}
