using CSharpFunctionalExtensions;
using Domain.Models;
using Domain.Stores;
using Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;

namespace Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeStore
    {
        private readonly DbAccessSystemContext _context;

        public EmployeeRepository(DbAccessSystemContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            return await _context.Employees.ToListAsync(cancellationToken);
        }

        public async Task<Result<Employee, Error>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            if (employee == null)
                return Errors.General.NotFound(id, "employee");

            return employee;
        }

        public async Task AddAsync(
            Employee employee,
            CancellationToken cancellationToken = default)
        {
            await _context.Employees.AddAsync(employee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Result<Employee, Error>> UpdateAsync(
            Guid id,
            Employee employee,
            CancellationToken cancellationToken = default)
        {
            var existingEmployee
                = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            if (existingEmployee == null)
                return Errors.General.NotFound(employee.Id, "employee"); 

            existingEmployee.FullName = employee.FullName;
            existingEmployee.Department = employee.Department;

            _context.Employees.Update(existingEmployee);
            await _context.SaveChangesAsync(cancellationToken);

            return existingEmployee;
        }

        public async Task<Result<Employee, Error>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            if (employee == null)
                return Errors.General.NotFound(id, "employee"); 

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync(cancellationToken);

            return employee;
        }
    }
}
