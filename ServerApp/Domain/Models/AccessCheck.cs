namespace Domain.Models;

public class AccessCheck
{
    public Guid Id { get; init; }
    public string LicensePlate { get; init; } = string.Empty;
    public DateTime CheckDateTime { get; init; } = DateTime.UtcNow;
    public bool IsAccessGranted { get; init; }

    public Guid? EmployeeId { get; init; }
    public Employee? Employee { get; init; }
}
