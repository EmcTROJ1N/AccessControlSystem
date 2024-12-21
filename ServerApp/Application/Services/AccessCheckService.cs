using CSharpFunctionalExtensions;
using Domain.Stores;
using Domain.Utils;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class AccessCheckService
    {
        private readonly IAccessCheckStore _accessCheckStore;
        private readonly ILogger<AccessCheckService> _logger;

        public AccessCheckService(IAccessCheckStore accessCheckStore, ILogger<AccessCheckService> logger)
        {
            _accessCheckStore = accessCheckStore;
            _logger = logger;
        }

        public async Task<UnitResult<Error>> PerformAccessCheckAsync(
            Guid employeeId,
            string licensePlate,
            CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Initiating access check.");

            var result = await _accessCheckStore.CheckAccess(employeeId, licensePlate, cancellationToken);
            if (result.IsFailure)
                return result.Error;

            _logger.LogInformation("{EmployeeId} - Access check completed successfully. Access: {licensePlate}",
                employeeId, licensePlate);

            return result;
        }
    }
}
