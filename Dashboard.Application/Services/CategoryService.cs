using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dashboard.Application.Contracts;
using Dashboard.Application.DTOS.CategoryDtos;
using Dashboard.Domain.Entities;
using Dashboard.Infrastrcuture.BaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.Services;


public class CategoryService(DashboardDbContext dbContext, IMapper mapper) : ICategoryService
{
    private readonly DashboardDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;


    public async Task AddCategoryAsync(List<CategoryRequestDto> categoryRequestDto)
    {
        var mappedCategory = _mapper.Map<List<Category>>(categoryRequestDto);
        await _dbContext.AddRangeAsync(mappedCategory);

        await _dbContext.SaveChangesAsync();


    }

    public async Task DeleteCategoryAsync(Guid id)
    {
        var category = await _dbContext.Categories.FindAsync(id);
        _dbContext.Categories.Remove(category);
        await _dbContext.SaveChangesAsync();


    }

    public async Task<List<CategoryResponseDto>> GetAllCategorysAsync()
    {
        var categorys = await _dbContext.Categories.ProjectTo<CategoryResponseDto>(_mapper.ConfigurationProvider).ToListAsync();

        return categorys;
    }

    public async Task<CategoryResponseDto> GetCategoryByIdAsync(Guid id)
    {
        var category = await _dbContext.Categories
         .ProjectTo<CategoryResponseDto>(_mapper.ConfigurationProvider)
         .FirstOrDefaultAsync(s => s.Id == id);


        return category;
    }

    public async Task<CategoryResponseDto> UpdateCategoryAsync(Guid id, CategoryRequestDto categoryRequestDto)
    {
        var category = await _dbContext.Categories.FindAsync(id);



        _mapper.Map(categoryRequestDto, category);

        await _dbContext.SaveChangesAsync();

        var categoryResponse = _mapper.Map<CategoryResponseDto>(category);




        return categoryResponse;
    }
}