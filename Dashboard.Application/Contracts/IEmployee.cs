using Dashboard.Core.Result;
using Dashboard.Application.DTOS.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.Contracts;

/// <summary>
/// Provides an interface for employee-related services that manages employee data across the application. Inherits from IApplicationService and IScopedService.
/// </summary>
public interface IEmployeeService 
{
    /// <summary>
    /// Asynchronously adds a new employee.
    /// </summary>
    /// <param name="employeeRequestDto">The data transfer object containing employee information.</param>
    /// <returns>A task representing the result of adding the employee successfully.</returns>
    public Task AddEmployeeAsync(EmployeeRequestDto employeeRequestDto);

    /// <summary>
    /// Asynchronously retrieves all employees.
    /// </summary>
    /// <returns>A task representing the result of retrieving a list of employee response data transfer objects.</returns>
    public Task<List<EmployeeResponseDto>> GetAllEmployeesAsync();

    /// <summary>
    /// Asynchronously retrieves a employee by its ID.
    /// </summary>
    /// <param name="id">The ID of the employee to retrieve.</param>
    /// <returns>A task representing the result of retrieving the employee response data transfer object.</returns>
    public Task<EmployeeResponseDto> GetEmployeeByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously updates a employee by its ID.
    /// </summary>
    /// <param name="id">The ID of the employee to update.</param>
    /// <param name="employeeRequestDto">The data transfer object containing updated employee information.</param>
    /// <returns>A task representing the result of updating the employee response data transfer object.</returns>
    public Task<EmployeeResponseDto>UpdateEmployeeAsync(Guid id, EmployeeRequestDto employeeRequestDto);

    /// <summary>
    /// Asynchronously deletes a employee by its ID.
    /// </summary>
    /// <param name="id">The ID of the employee to delete.</param>
    /// <returns>A task representing the result of deleting the employee successfully.</returns>
    public Task DeleteEmployeeAsync(Guid id);

  
}
