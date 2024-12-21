namespace Domain.Models;

public class Employee
{
    public Guid Id { get; init; }
    public string FullName { get; set; } = string.Empty;
    public string? Department { get; set; }

    public ICollection<Car> Cars { get; init; } = new List<Car>();
}
