using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dashboard.Application.Contracts;
using Dashboard.Application.DTOS.EmployeeDtos;
using Dashboard.Domain.Entities;
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

    public async Task AddEmployeeAsync(EmployeeRequestDto employeeRequestDto)
    {
        var mappedEmployee = _mapper.Map<Employee>(employeeRequestDto);
         await _dbContext.AddAsync(mappedEmployee);
       
        await _dbContext.SaveChangesAsync();
    
      
    }

    public async Task DeleteEmployeeAsync(Guid id)
    {
        var employee = await _dbContext.Employees.FindAsync(id);

       

        _dbContext.Employees.Remove(employee);
        await _dbContext.SaveChangesAsync();


    }

    public async Task<List<EmployeeResponseDto>> GetAllEmployeesAsync()
    {
        var employees = await _dbContext.Employees.ProjectTo<EmployeeResponseDto>(_mapper.ConfigurationProvider).ToListAsync();
       
        return employees;
    }

    public async Task<EmployeeResponseDto> GetEmployeeByIdAsync(Guid id)
    {
        var employee = await _dbContext.Employees
         .ProjectTo<EmployeeResponseDto>(_mapper.ConfigurationProvider)
         .FirstOrDefaultAsync(s => s.Id == id);
       

        return employee;
    }

    public async Task<EmployeeResponseDto> UpdateEmployeeAsync(Guid id, EmployeeRequestDto employeeRequestDto)
    {
        var employee = await _dbContext.Employees.FindAsync(id);

       

        _mapper.Map(employeeRequestDto, employee);
   
        await _dbContext.SaveChangesAsync();

        var employeeResponse = _mapper.Map<EmployeeResponseDto>(employee);
        

    

        return employeeResponse;
    }
}
