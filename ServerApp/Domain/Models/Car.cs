namespace Domain.Models;

public class Car
{
    public Guid Id { get; init; }
    public string LicensePlate { get; set; } = string.Empty;
    public Guid EmployeeId { get; set; }
    public Employee Employee { get; init; } = null!;
}