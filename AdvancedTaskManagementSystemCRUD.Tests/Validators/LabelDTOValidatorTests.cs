using AdvancedTaskManagementSystemCRUD.Application.Repository;
using AdvancedTaskManagementSystemCRUD.Application.Repository.DTOs;
using AdvancedTaskManagementSystemCRUD.Application.Services.Configuration.Validators;
using AdvancedTaskManagementSystemCRUD.Application.Services.Configurations.DTOs;
using Moq;

namespace AdvancedTaskManagementSystemCRUD.Tests.Validators;

public class LabelDTOValidatorTests
{
    private readonly Mock<IConfigurationRepository> _configurationRepository = new();

    [Fact]
    public void Validate_WhenLabelIsUnique_ShouldBeValid()
    {
        _configurationRepository.Setup(repository => repository.GetLabels()).Returns(
            new List<LabelEntityDTO>
            {
                new() { Id = 1, Name = "bug" }
            });

        var validator = new LabelDTOValidator(_configurationRepository.Object);
        var request = new LabelDTO
        {
            Name = "feature",
            Description = "Nowa funkcjonalność"
        };

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void Validate_WhenLabelNameAlreadyExists_ShouldReturnValidationError()
    {
        _configurationRepository.Setup(repository => repository.GetLabels()).Returns(
            new List<LabelEntityDTO>
            {
                new() { Id = 1, Name = "bug" }
            });

        var validator = new LabelDTOValidator(_configurationRepository.Object);
        var request = new LabelDTO
        {
            Name = "BUG",
            Description = "Duplikat"
        };

        var result = validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, error => error.ErrorMessage == "Etykieta o podanej nazwie już istnieje.");
    }

    [Fact]
    public void Validate_WhenEditingExistingLabelWithSameName_ShouldBeValid()
    {
        _configurationRepository.Setup(repository => repository.GetLabels()).Returns(
            new List<LabelEntityDTO>
            {
                new() { Id = 7, Name = "bug" }
            });

        var validator = new LabelDTOValidator(_configurationRepository.Object);
        var request = new LabelDTO
        {
            Id = 7,
            Name = "bug",
            Description = "Zmieniony opis"
        };

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }
}
