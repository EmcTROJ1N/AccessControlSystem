using Application.Services;
using CSharpFunctionalExtensions;
using Domain.Models;
using Domain.Stores;
using Domain.Utils;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ApplicationTests;

public class CarServiceTests
{
    private readonly Mock<ICarStore> _carStoreMock;
    private readonly Mock<ILogger<CarService>> _loggerMock;
    private readonly CarService _carService;

    public CarServiceTests()
    {
        _carStoreMock = new Mock<ICarStore>();
        _loggerMock = new Mock<ILogger<CarService>>();
        _carService = new CarService(_carStoreMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAllCarsAsync_ShouldReturnAllCars()
    {
        // Arrange
        var cars = new List<Car> { new() { Id = Guid.NewGuid(), LicensePlate = "123ABC" } };
        _carStoreMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(cars);

        // Act
        var result = await _carService.GetAllCarsAsync();

        // Assert
        result.Should().BeEquivalentTo(cars);
        _carStoreMock.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetCarByIdAsync_ShouldReturnCar_WhenCarExists()
    {
        // Arrange
        var car = new Car { Id = Guid.NewGuid(), LicensePlate = "123ABC" };
        _carStoreMock.Setup(repo => repo.GetByIdAsync(car.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<Car, Error>(car));

        // Act
        var result = await _carService.GetCarByIdAsync(car.Id);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(car);
        _carStoreMock.Verify(repo => repo.GetByIdAsync(car.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task GetCarByIdAsync_ShouldReturnError_WhenCarDoesNotExist()
    {
        // Arrange
        var carId = Guid.NewGuid();
        _carStoreMock.Setup(repo => repo.GetByIdAsync(carId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<Car, Error>(Errors.General.NotFound(carId, "car")));

        // Act
        var result = await _carService.GetCarByIdAsync(carId);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(Errors.General.NotFound(carId, "car"));
    }

    [Fact]
    public async Task AddCarAsync_ShouldAddCar_WhenCarIsValid()
    {
        // Arrange
        var car = new Car { Id = Guid.NewGuid(), LicensePlate = "123ABC" };
        _carStoreMock.Setup(repo => repo.CarExistsAsync(car.LicensePlate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        _carStoreMock.Setup(repo => repo.AddAsync(car, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<Car, Error>(car));

        // Act
        var result = await _carService.AddCarAsync(car);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(car);
    }

    [Fact]
    public async Task AddCarAsync_ShouldReturnError_WhenLicensePlateIsEmpty()
    {
        // Arrange
        var car = new Car { Id = Guid.NewGuid(), LicensePlate = "" };

        // Act
        var result = await _carService.AddCarAsync(car);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(Errors.General.LicensePlateIsEmpty());
    }

    [Fact]
    public async Task AddCarAsync_ShouldReturnError_WhenCarAlreadyExists()
    {
        // Arrange
        var car = new Car { Id = Guid.NewGuid(), LicensePlate = "123ABC" };
        _carStoreMock.Setup(repo => repo.CarExistsAsync(car.LicensePlate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _carService.AddCarAsync(car);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(Errors.General.AlreadyExist("car"));
    }

    [Fact]
    public async Task UpdateCarAsync_ShouldUpdateCar_WhenCarExists()
    {
        // Arrange
        var car = new Car { Id = Guid.NewGuid(), LicensePlate = "123ABC" };
        _carStoreMock.Setup(repo => repo.GetByIdAsync(car.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<Car, Error>(car));
        _carStoreMock.Setup(repo => repo.UpdateAsync(car.Id, car, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<Car, Error>(car));

        // Act
        var result = await _carService.UpdateCarAsync(car.Id, car);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(car);
    }

    [Fact]
    public async Task DeleteCarAsync_ShouldDeleteCar_WhenCarExists()
    {
        // Arrange
        var car = new Car { Id = Guid.NewGuid(), LicensePlate = "123ABC" };
        _carStoreMock.Setup(repo => repo.GetByIdAsync(car.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<Car, Error>(car));
        _carStoreMock.Setup(repo => repo.DeleteAsync(car.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<Car, Error>(car));

        // Act
        var result = await _carService.DeleteCarAsync(car.Id);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task CheckCarAccessAsync_ShouldReturnSuccess_WhenAccessIsGranted()
    {
        // Arrange
        var licensePlate = "123ABC";
        _carStoreMock.Setup(repo => repo.GetByLicensePlateAsync(licensePlate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _carService.CheckCarAccessAsync(licensePlate);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeTrue();
    }

    [Fact]
    public async Task CheckCarAccessAsync_ShouldReturnError_WhenAccessIsDenied()
    {
        // Arrange
        var licensePlate = "123ABC";
        _carStoreMock.Setup(repo => repo.GetByLicensePlateAsync(licensePlate, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        // Act
        var result = await _carService.CheckCarAccessAsync(licensePlate);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(Errors.General.AccessDenied(licensePlate));
    }
}
