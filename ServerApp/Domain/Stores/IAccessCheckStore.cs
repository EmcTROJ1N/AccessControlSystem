using CSharpFunctionalExtensions;
using Domain.Utils;

namespace Domain.Stores;

public interface IAccessCheckStore
{
    Task<UnitResult<Error>> CheckAccess(
        Guid employeeId,
        string licensePlate,
        CancellationToken cancellationToken = default);
}
