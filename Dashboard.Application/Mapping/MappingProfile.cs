using AutoMapper;
using Dashboard.Application.DTOS.Brands;
using Dashboard.Application.DTOS.CategoryDtos;
using Dashboard.Application.DTOS.EmployeeDtos;
using Dashboard.Application.DTOS.ProductDtos;
using Dashboard.Application.DTOS.VendorDtos;
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
        CreateMap<EmployeeRequestDto, Employee>().ReverseMap();
        CreateMap<Employee, EmployeeResponseDto>().ReverseMap();


        CreateMap<ProductRequestDto, Product>().ReverseMap();
        CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.Name))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(des => des.VendorName, opt => opt.MapFrom(src => src.Vendor.Name)).ReverseMap();

        CreateMap<CategoryRequestDto, Category>().ReverseMap();
        CreateMap<Category, CategoryResponseDto>().ReverseMap();

        CreateMap<BrandRequestDto, Brand>().ReverseMap();
        CreateMap<Brand, BrandResponseDto>().ReverseMap();

        CreateMap<VendorRequestDto, Vendor>().ReverseMap();
        CreateMap<Vendor, VendorResponseDto>().ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products)).ReverseMap();




    }
}
