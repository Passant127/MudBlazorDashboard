using BenchmarkDotNet.Attributes;
using Dashboard.Application.DTOS.EmployeeDtos;
using Dashboard.Application.Services;
using Dashboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Toolchains.InProcess.Emit;
using AutoMapper;
using Dashboard.Infrastrcuture.BaseContext;
using Microsoft.EntityFrameworkCore;
namespace Dashboard.Benchmark;




[InProcess]
[Config(typeof(InProcessConfig))]
[SimpleJob(launchCount: 1, warmupCount: 5, iterationCount: 10)]
public class EmployeeServiceBenchmark
{
    private EmployeeService _service;
    private IMapper _mapper;
    private DashboardDbContext _dbContext;

    [GlobalSetup]
    public void Setup()
    {
        // Configure AutoMapper
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<EmployeeRequestDto, Employee>();
            cfg.CreateMap<Employee, EmployeeResponseDto>();
        });

        _mapper = config.CreateMapper();

        // Configure SQL Server database connection
        var options = new DbContextOptionsBuilder<DashboardDbContext>()
            .UseSqlServer("DefaultConnection")  // Replace with your SQL Server connection string
            .Options;

        _dbContext = new DashboardDbContext(options);

        // Initialize the service with the real DbContext and AutoMapper
        _service = new EmployeeService(_dbContext, _mapper);

        // No data seeding here
    }

    [Benchmark]
    public async Task BenchmarkGetAllEmployeesAsync()
    {
        await _service.GetAllEmployeesAsync();
    }
}



