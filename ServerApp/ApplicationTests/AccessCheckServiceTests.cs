using Application.Services;
using CSharpFunctionalExtensions;
using Domain.Stores;
using Domain.Utils;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ApplicationTests;

public class AccessCheckServiceTests
{
    private readonly Mock<IAccessCheckStore> _accessCheckStoreMock;
    private readonly Mock<ILogger<AccessCheckService>> _loggerMock;
    private readonly AccessCheckService _accessCheckService;

    public AccessCheckServiceTests()
    {
        _accessCheckStoreMock = new Mock<IAccessCheckStore>();
        _loggerMock = new Mock<ILogger<AccessCheckService>>();
        _accessCheckService = new AccessCheckService(_accessCheckStoreMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task PerformAccessCheckAsync_ShouldReturnSuccess_WhenAccessIsGranted()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var licensePlate = "123ABC";
        _accessCheckStoreMock
            .Setup(store => store.CheckAccess(employeeId, licensePlate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(UnitResult.Success<Error>());

        // Act
        var result = await _accessCheckService.PerformAccessCheckAsync(employeeId, licensePlate);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task PerformAccessCheckAsync_ShouldReturnError_WhenAccessIsDenied()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var licensePlate = "123ABC";
        var accessDeniedError = Errors.General.AccessDenied(licensePlate);

        _accessCheckStoreMock
            .Setup(store => store.CheckAccess(employeeId, licensePlate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(UnitResult.Failure(accessDeniedError));

        // Act
        var result = await _accessCheckService.PerformAccessCheckAsync(employeeId, licensePlate);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(accessDeniedError);
    }
}
