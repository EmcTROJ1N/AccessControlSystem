using CSharpFunctionalExtensions;
using Domain.Models;
using Domain.Stores;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CarService
{
    private readonly ICarStore _carRepository;
    private readonly ILogger<CarService> _logger;

    public CarService(ICarStore carRepository, ILogger<CarService> logger)
    {
        _carRepository = carRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Car>> GetAllCarsAsync(CancellationToken cancellationToken = default)
    {
        return await _carRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Result<Car, Error>> GetCarByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _carRepository.GetByIdAsync(id, cancellationToken);
        if (result.IsFailure)
            return result.Error;

        _logger.LogInformation("Car with ID {Id} retrieved successfully.", id);

        return result;
    }

    public async Task<Result<Car, Error>> AddCarAsync(Car car, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(car.LicensePlate))
            return Errors.General.LicensePlateIsEmpty();

        var existingCar = await _carRepository.CarExistsAsync(car.LicensePlate, cancellationToken);
        if (existingCar)
            return Errors.General.AlreadyExist("car");

        var result = await _carRepository.AddAsync(car, cancellationToken);
        if (result.IsFailure)
            return result.Error;

        _logger.LogInformation("Car with license plate {LicensePlate} added successfully.", car.LicensePlate);

        return result;
    }


    public async Task<Result<Car, Error>> UpdateCarAsync(
        Guid id, Car car, CancellationToken cancellationToken = default)
    {
        var existingCar = await _carRepository.GetByIdAsync(id, cancellationToken);
        if (existingCar.IsFailure)
            return existingCar.Error;

        var result = await _carRepository.UpdateAsync(id, car, cancellationToken);
        if (result.IsFailure)
            return result.Error;

        _logger.LogInformation("Car with ID {Id} updated successfully.", id);

        return result;
    }

    public async Task<Result<Car, Error>> DeleteCarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var car = await _carRepository.GetByIdAsync(id, cancellationToken);
        if (car.IsFailure)
            return car.Error;

        var result = await _carRepository.DeleteAsync(id, cancellationToken);
        if (result.IsFailure)
            return result.Error;

        _logger.LogInformation("Car with ID {Id} deleted successfully.", id);

        return result;
    }

    public async Task<Result<bool, Error>> CheckCarAccessAsync(
        string licensePlate,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
            return Errors.General.ValueIsRequired("license plate");


        var carAccess = await _carRepository.GetByLicensePlateAsync(licensePlate, cancellationToken);
        if (carAccess == false)
            return Errors.General.AccessDenied(licensePlate);

        _logger.LogInformation("Access granted for car with license plate {LicensePlate}.", licensePlate);

        return Result.Success<bool, Error>(true);
    }
}
