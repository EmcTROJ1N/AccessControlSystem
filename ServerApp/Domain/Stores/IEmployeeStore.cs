using CSharpFunctionalExtensions;
using Domain.Models;
using Domain.Utils;

namespace Domain.Stores;

public interface IEmployeeStore
{
    Task<IEnumerable<Employee>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<Employee, Error>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Employee employee, CancellationToken cancellationToken = default);
    Task<Result<Employee, Error>> UpdateAsync(Guid id, Employee employee, CancellationToken cancellationToken = default);
    Task<Result<Employee, Error>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
