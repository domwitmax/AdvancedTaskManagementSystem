using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Services.Account.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Account.Validators;
using Moq;

namespace AdvancedTaskManagementSystemCRUD.Tests.Validators;

public class RegisterDTOValidatorTests
{
    private readonly Mock<IAccountRepository> _accountRepository = new();

    [Fact]
    public void Validate_WhenDataIsCorrect_ShouldBeValid()
    {
        _accountRepository.Setup(repository => repository.IsLoginTaken("student")).Returns(false);
        _accountRepository.Setup(repository => repository.IsEmailTaken("student@example.com")).Returns(false);

        var validator = new RegisterDTOValidator(_accountRepository.Object);
        var request = new RegisterDTO
        {
            Login = "student",
            Email = "student@example.com",
            Password = "Password123"
        };

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WhenLoginIsTaken_ShouldReturnValidationError()
    {
        _accountRepository.Setup(repository => repository.IsLoginTaken("student")).Returns(true);
        _accountRepository.Setup(repository => repository.IsEmailTaken(It.IsAny<string>())).Returns(false);

        var validator = new RegisterDTOValidator(_accountRepository.Object);
        var request = new RegisterDTO
        {
            Login = "student",
            Email = "student@example.com",
            Password = "Password123"
        };

        var result = validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
            error.PropertyName == nameof(RegisterDTO.Login) &&
            error.ErrorMessage == "Login jest już zajęty.");
    }

    [Fact]
    public void Validate_WhenEmailHasInvalidFormat_ShouldReturnValidationError()
    {
        _accountRepository.Setup(repository => repository.IsLoginTaken(It.IsAny<string>())).Returns(false);

        var validator = new RegisterDTOValidator(_accountRepository.Object);
        var request = new RegisterDTO
        {
            Login = "student",
            Email = "invalid-email",
            Password = "Password123"
        };

        var result = validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
            error.PropertyName == nameof(RegisterDTO.Email) &&
            error.ErrorMessage == "Email ma niepoprawny format.");
    }

    [Fact]
    public void Validate_WhenPasswordIsTooShort_ShouldReturnValidationError()
    {
        _accountRepository.Setup(repository => repository.IsLoginTaken(It.IsAny<string>())).Returns(false);
        _accountRepository.Setup(repository => repository.IsEmailTaken(It.IsAny<string>())).Returns(false);

        var validator = new RegisterDTOValidator(_accountRepository.Object);
        var request = new RegisterDTO
        {
            Login = "student",
            Email = "student@example.com",
            Password = "12345"
        };

        var result = validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error =>
            error.PropertyName == nameof(RegisterDTO.Password) &&
            error.ErrorMessage == "Hasło musi mieć co najmniej 6 znaków.");
    }
}
