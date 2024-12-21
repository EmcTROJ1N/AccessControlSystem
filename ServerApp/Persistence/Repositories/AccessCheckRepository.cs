using CSharpFunctionalExtensions;
using Domain.Stores;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;

namespace Persistence.Repositories;

public class AccessCheckRepository : IAccessCheckStore
{
    private readonly DbAccessSystemContext _context;

    public AccessCheckRepository(DbAccessSystemContext context)
    {
        _context = context;
    }

    public async Task<UnitResult<Error>> CheckAccess(
        Guid employeeId, string licensePlate, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(licensePlate))
            return Errors.General.ValueIsRequired("license plate");

        var employeeExists = await _context.Employees.AnyAsync(e => e.Id == employeeId, cancellationToken);
        if (!employeeExists)
            return Errors.General.NotFound(employeeId);

        var carExists = await _context.Cars.AnyAsync(c => c.EmployeeId == employeeId && c.LicensePlate == licensePlate,
            cancellationToken);
        if (!carExists)
            return Errors.General.AccessDenied(licensePlate);

        return Result.Success<Error>();
    }
}
