using AutoMapper;
using Dashboard.Application.Contracts;
using Dashboard.Application.DTOS.Brands;
using Dashboard.Domain.Entities;
using Dashboard.Infrastrcuture.BaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Application.Services;


public class BrandService(DashboardDbContext dbContext, IMapper mapper) : IBrandService
{
    private readonly DashboardDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;


    public async Task AddBrandAsync(List<BrandRequestDto> brandRequestDto)
    {
        var mappedBrand = _mapper.Map<List<Brand>>(brandRequestDto);
        await _dbContext.AddRangeAsync(mappedBrand);

        await _dbContext.SaveChangesAsync();


    }

    public async Task DeleteBrandAsync(Guid id)
    {
        var brand = await _dbContext.Brands.FindAsync(id);
        _dbContext.Brands.Remove(brand);
        await _dbContext.SaveChangesAsync();


    }

    public async Task<List<BrandResponseDto>> GetAllBrandsAsync()
    {
        var brands = await _dbContext.Brands.ProjectTo<BrandResponseDto>(_mapper.ConfigurationProvider).ToListAsync();

        return brands;
    }

    public async Task<BrandResponseDto> GetBrandByIdAsync(Guid id)
    {
        var brand = await _dbContext.Brands
         .ProjectTo<BrandResponseDto>(_mapper.ConfigurationProvider)
         .FirstOrDefaultAsync(s => s.Id == id);


        return brand;
    }

    public async Task<BrandResponseDto> UpdateBrandAsync(Guid id, BrandRequestDto brandRequestDto)
    {
        var brand = await _dbContext.Brands.FindAsync(id);



        _mapper.Map(brandRequestDto, brand);

        await _dbContext.SaveChangesAsync();

        var brandResponse = _mapper.Map<BrandResponseDto>(brand);

        return brandResponse;
    }
}
