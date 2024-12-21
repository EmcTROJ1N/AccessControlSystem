using API.Contracts;
using API.Extensions;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EmployeesController : ApplicationController
    {
        private readonly EmployeeService _employeeService;

        public EmployeesController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _employeeService.GetAllEmployeesAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _employeeService.GetEmployeeByIdAsync(id, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] EmployeeCreateRequest request,
            CancellationToken cancellationToken)
        {
            var employee = new Employee { FullName = request.FullName, Department = request.Department };

            var result = await _employeeService.AddEmployeeAsync(employee, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] EmployeeUpdateRequest request,
            CancellationToken cancellationToken)
        {
            var employeeForUpdate = new Employee { FullName = request.FullName, Department = request.Department };
            var result = await _employeeService.UpdateEmployeeAsync(id, employeeForUpdate, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _employeeService.DeleteEmployeeAsync(id, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok();
        }
    }
}
