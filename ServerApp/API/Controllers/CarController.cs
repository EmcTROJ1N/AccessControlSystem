using API.Contracts;
using API.Extensions;
using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CarController : ApplicationController
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _carService.GetAllCarsAsync(cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var result = await _carService.GetCarByIdAsync(id, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CarCreateRequest request,
            CancellationToken cancellationToken)
        {
            var car = new Car { EmployeeId = request.EmployeeId, LicensePlate = request.LicensePlate };
            var result = await _carService.AddCarAsync(car, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(
            [FromRoute] Guid id,
            [FromBody] CarUpdateRequest request,
            CancellationToken cancellationToken)
        {
            var carForUpdate = new Car { EmployeeId = request.EmployeeId, LicensePlate = request.LicensePlate };
            var result = await _carService.UpdateCarAsync(id, carForUpdate, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken token)
        {
            var result = await _carService.DeleteCarAsync(id, token);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(result.Value);
        }
    }
}
