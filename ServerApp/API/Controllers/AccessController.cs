using API.Extensions;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccessController : ApplicationController
    {
        private readonly AccessCheckService _accessCheckService;

        public AccessController(AccessCheckService accessCheckService)
        {
            _accessCheckService = accessCheckService;
        }

        [HttpGet]
        public async Task<IActionResult> CheckAccess(Guid id, string licensePlate, CancellationToken cancellationToken)
        {
            var result = await _accessCheckService.PerformAccessCheckAsync(id, licensePlate, cancellationToken);
            if (result.IsFailure)
                return result.Error.ToResponse();

            return Ok(true);
        }
    }
}
