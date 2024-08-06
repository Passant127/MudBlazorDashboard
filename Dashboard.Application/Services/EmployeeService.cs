using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dashboard.Application.Contracts;
using Dashboard.Application.DTOS.EmployeeDtos;
using Dashboard.Core.Entities;
using Dashboard.Core.Result;
using Dashboard.Infrastrcuture.BaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.Services;

public class EmployeeService(DashboardDbContext dbContext , IMapper mapper) : IEmployeeService
{
    private readonly DashboardDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> AddEmployeeAsync(EmployeeRequestDto employeeRequestDto)
    {
        var mappedEmployee = _mapper.Map<Employee>(employeeRequestDto);
        if (mappedEmployee is null)
        {
            
            return Result.Invalid(new List<ValidationError>
            {
                new ValidationError
                {
                    ErrorMessage = "Validation Errror"
                }
            });
        }
    
        _dbContext.Employees.Add(mappedEmployee);
        await _dbContext.SaveChangesAsync();
    
        return Result.SuccessWithMessage("employee added successfully");
    }

    public async Task<Result> DeleteEmployeeAsync(Guid id)
    {
        var employee = await _dbContext.Employees.FindAsync(id);

        if (employee is null)
        {
          
            return Result.NotFound(["employee Invaild Id"]);
        }

        _dbContext.Employees.Remove(employee);
        await _dbContext.SaveChangesAsync();


        return Result.SuccessWithMessage("employee removed successfully");
    }

    public async Task<Result<List<EmployeeResponseDto>>> GetAllEmployeesAsync()
    {
        var employees = await _dbContext.Employees.ProjectTo<EmployeeResponseDto>(_mapper.ConfigurationProvider).ToListAsync();
       
        return Result.Success(employees);
    }

    public async Task<Result<EmployeeResponseDto>> GetEmployeeByIdAsync(Guid id)
    {
        var employee = await _dbContext.Employees
         .ProjectTo<EmployeeResponseDto>(_mapper.ConfigurationProvider)
         .FirstOrDefaultAsync(s => s.Id == id);
        if (employee is null)
        {
           
            return Result.NotFound(["employee not found"]);
        }

        return Result.Success(employee);
    }

    public async Task<Result<EmployeeResponseDto>> UpdateEmployeeAsync(Guid id, EmployeeRequestDto employeeRequestDto)
    {
        var employee = await _dbContext.Employees.FindAsync(id);

        if (employee is null)
        {
          
            return Result.NotFound(["employee not found"]);
        }

        _mapper.Map(employeeRequestDto, employee);
   
        await _dbContext.SaveChangesAsync();

        var employeeResponse = _mapper.Map<EmployeeResponseDto>(employee);
        if (employeeResponse is null)
        {

            return Result.Invalid(new List<ValidationError>
                {
                    new ValidationError
                    {
                        ErrorMessage = "Validation Errror"
                    }
                });
        }

    

        return Result.Success(employeeResponse);
    }
}
