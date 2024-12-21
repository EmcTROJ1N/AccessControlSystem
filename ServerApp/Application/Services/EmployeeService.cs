using CSharpFunctionalExtensions;
using Domain.Models;
using Domain.Stores;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeStore _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeStore employeeRepository, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(CancellationToken token = default)
        {
            return await _employeeRepository.GetAllAsync(token);
        }

        public async Task<Result<Employee, Error>> GetEmployeeByIdAsync(Guid id, CancellationToken token = default)
        {
            var result = await _employeeRepository.GetByIdAsync(id, token);

            if (result.IsFailure)
                return result.Error;

            return result;
        }

        public async Task<UnitResult<Error>> AddEmployeeAsync(Employee employee, CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(employee.FullName))
                return Errors.General.ValueIsRequired("full name");

            await _employeeRepository.AddAsync(employee, token);

            _logger.LogInformation("Employee {FullName} added successfully.", employee.FullName);

            return UnitResult.Success<Error>();
        }

        public async Task<Result<Employee, Error>> UpdateEmployeeAsync(
            Guid id,
            Employee employee,
            CancellationToken token = default)
        {
            var result = await _employeeRepository.GetByIdAsync(id, token);
            if (result.IsFailure)
                return result.Error;

            var updatedEmployee = await _employeeRepository.UpdateAsync(id, employee, token);

            if (updatedEmployee.IsFailure)
                return updatedEmployee.Error;

            _logger.LogInformation("Employee with ID {Id} updated successfully.", id);

            return updatedEmployee;
        }

        public async Task<Result<Employee, Error>> DeleteEmployeeAsync(Guid id, CancellationToken token = default)
        {
            var employee = await _employeeRepository.GetByIdAsync(id, token);
            if (employee.IsFailure)
                return employee.Error;

            var result = await _employeeRepository.DeleteAsync(id, token);
            if (result.IsFailure)
                return result.Error;

            _logger.LogInformation("Employee with ID {Id} deleted successfully.", id);

            return employee;
        }
    }
}
