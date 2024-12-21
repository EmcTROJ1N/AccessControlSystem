using CSharpFunctionalExtensions;
using Domain.Models;
using Domain.Stores;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;

namespace Persistence.Repositories;

public class CarRepository : ICarStore
{
    private readonly DbAccessSystemContext _context;

    public CarRepository(DbAccessSystemContext accessSystemContext)
    {
        _context = accessSystemContext;
    }

    public async Task<List<Car>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Cars.ToListAsync(cancellationToken);
    }

    public async Task<Result<Car, Error>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        if (car == null)
            return Errors.General.NotFound(id, "car");

        return car;
    }

    public async Task<bool> CarExistsAsync(string licensePlate, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
            return false;

        return await _context.Cars.AnyAsync(c => c.LicensePlate == licensePlate, cancellationToken);
    }

    public async Task<Result<Car, Error>> AddAsync(Car car, CancellationToken cancellationToken = default)
    {
        var isCarExist
            = await _context.Cars.FirstOrDefaultAsync(c => c.Id == car.Id, cancellationToken);
        if (isCarExist is not null)
            return Errors.General.AlreadyExist("car");

        await _context.Cars.AddAsync(car, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return car;
    }

    public async Task<Result<Car, Error>> UpdateAsync(Guid id, Car car, CancellationToken cancellationToken = default)
    {
        var existingCar = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        if (existingCar == null)
            return Errors.General.NotFound(id, "car");
        
        existingCar.LicensePlate = car.LicensePlate;
        existingCar.EmployeeId = car.EmployeeId;

        _context.Cars.Update(existingCar);
        await _context.SaveChangesAsync(cancellationToken);

        return existingCar;
    }

    public async Task<Result<Car, Error>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        if (car == null)
            return Errors.General.NotFound(id, "car");
        
        _context.Cars.Remove(car);
        await _context.SaveChangesAsync(cancellationToken);

        return car;
    }

    public async Task<bool> GetByLicensePlateAsync(string licensePlate, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
            return false;

        return await _context.Cars.AnyAsync(c => c.LicensePlate == licensePlate, cancellationToken);
    }
}
