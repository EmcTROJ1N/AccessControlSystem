using Application.Services;
using CSharpFunctionalExtensions;
using Domain.Models;
using Domain.Stores;
using Domain.Utils;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace ApplicationTests;

public class EmployeeServiceTests
{
    private readonly Mock<IEmployeeStore> _employeeStoreMock;
    private readonly Mock<ILogger<EmployeeService>> _loggerMock;
    private readonly EmployeeService _employeeService;

    public EmployeeServiceTests()
    {
        _employeeStoreMock = new Mock<IEmployeeStore>();
        _loggerMock = new Mock<ILogger<EmployeeService>>();
        _employeeService = new EmployeeService(_employeeStoreMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GetAllEmployeesAsync_ShouldReturnAllEmployees()
    {
        // Arrange
        var employees = new List<Employee>
        {
            new() { Id = Guid.NewGuid(), FullName = "John Doe", Department = "IT" },
            new() { Id = Guid.NewGuid(), FullName = "Jane Smith", Department = "HR" }
        };

        _employeeStoreMock.Setup(store => store.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(employees);

        // Act
        var result = await _employeeService.GetAllEmployeesAsync();

        // Assert
        result.Should().BeEquivalentTo(employees);
    }

    [Fact]
    public async Task GetEmployeeByIdAsync_ShouldReturnEmployee_WhenEmployeeExists()
    {
        // Arrange
        var employee = new Employee { Id = Guid.NewGuid(), FullName = "John Doe", Department = "IT" };

        _employeeStoreMock.Setup(store => store.GetByIdAsync(employee.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<Employee, Error>(employee));

        // Act
        var result = await _employeeService.GetEmployeeByIdAsync(employee.Id);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(employee);
    }

    [Fact]
    public async Task GetEmployeeByIdAsync_ShouldReturnError_WhenEmployeeDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();
        _employeeStoreMock.Setup(store => store.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<Employee, Error>(Errors.General.NotFound(id, "employee")));

        // Act
        var result = await _employeeService.GetEmployeeByIdAsync(id);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(Errors.General.NotFound(id, "employee"));
    }

    [Fact]
    public async Task AddEmployeeAsync_ShouldAddEmployee_WhenValidEmployeeIsProvided()
    {
        // Arrange
        var employee = new Employee { Id = Guid.NewGuid(), FullName = "John Doe", Department = "IT" };

        _employeeStoreMock.Setup(store => store.AddAsync(employee, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _employeeService.AddEmployeeAsync(employee);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task AddEmployeeAsync_ShouldReturnError_WhenFullNameIsEmpty()
    {
        // Arrange
        var employee = new Employee { Id = Guid.NewGuid(), FullName = "", Department = "IT" };

        // Act
        var result = await _employeeService.AddEmployeeAsync(employee);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(Errors.General.ValueIsRequired("full name"));
    }

    [Fact]
    public async Task UpdateEmployeeAsync_ShouldUpdateEmployee_WhenEmployeeExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var existingEmployee = new Employee { Id = id, FullName = "John Doe", Department = "IT" };
        var updatedEmployee = new Employee { Id = id, FullName = "John Updated", Department = "IT" };

        _employeeStoreMock.Setup(store => store.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<Employee, Error>(existingEmployee));
        _employeeStoreMock.Setup(store => store.UpdateAsync(updatedEmployee, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<Employee, Error>(updatedEmployee));

        // Act
        var result = await _employeeService.UpdateEmployeeAsync(id, updatedEmployee);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(updatedEmployee);
    }

    [Fact]
    public async Task DeleteEmployeeAsync_ShouldDeleteEmployee_WhenEmployeeExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var employee = new Employee { Id = id, FullName = "John Doe", Department = "IT" };

        _employeeStoreMock.Setup(store => store.GetByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<Employee, Error>(employee));
        _employeeStoreMock.Setup(store => store.DeleteAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success<Employee, Error>(employee));

        // Act
        var result = await _employeeService.DeleteEmployeeAsync(id);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(employee);
    }
}
