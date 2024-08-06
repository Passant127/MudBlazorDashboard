using AutoMapper;
using Dashboard.Application.DTOS.EmployeeDtos;
using Dashboard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<EmployeeRequestDto, Employee>();
        CreateMap<Employee, EmployeeResponseDto>();
    }
}
