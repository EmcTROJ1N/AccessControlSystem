using CSharpFunctionalExtensions;
using Domain.Models;
using Domain.Utils;

namespace Domain.Stores;

public interface ICarStore
{
    Task<List<Car>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<Car,Error>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> GetByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken = default);
    Task<Result<Car, Error>> AddAsync(Car car, CancellationToken cancellationToken = default);
    Task<bool> CarExistsAsync(string licensePlate, CancellationToken cancellationToken = default);
    Task<Result<Car, Error>> UpdateAsync(Guid id, Car car, CancellationToken cancellationToken = default);
    Task<Result<Car, Error>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}