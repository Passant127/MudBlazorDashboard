using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure;
using Dashboard.Application.Contracts;
using Dashboard.Application.DTOS.ProductDtos;
using Dashboard.Application.SearchingCriteria.ProductSearchCriteria;
using Dashboard.Application.Sorting;
using Dashboard.Domain.Entities;
using Dashboard.Infrastrcuture.BaseContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Dashboard.Application.Services;

public class ProductService(DashboardDbContext dbContext, IMapper mapper , ISortingService sortingService) : IProductService
{
    private readonly DashboardDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly ISortingService _sortingService = sortingService;

    public async Task AddProductAsync(List<ProductRequestDto> productRequestDto)
    {
        var mappedProducts = _mapper.Map<List<Product>>(productRequestDto);
        await _dbContext.AddRangeAsync(mappedProducts);
        await _dbContext.SaveChangesAsync();

    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await _dbContext.Products.FindAsync(id);
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();

    }

    public async Task<List<ProductResponseDto>> GetAllProductsAsync(List<SortingDefinition> sortingDefinitions)
    {
        var products = await _dbContext.Products.ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider).ToListAsync();
        var sortedProduct = await _sortingService.SortItems(products, sortingDefinitions);
        if (sortedProduct == null)
            return products;
        return sortedProduct;
    }

    public async Task<List<ProductResponseDto>> GetAllProductsAsync(int page, int count , List<SortingDefinition> sortingDefinitions)
    {
        var sortedProduct =  await GetAllProductsAsync(sortingDefinitions);

        var products = sortedProduct
            .Skip(page*count)       
            .Take(count).ToList();

        return products ;

    }

    public async Task<ProductResponseDto> GetProductByIdAsync(Guid id)
    {
        var product = await _dbContext.Products
                 .ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(s => s.Id == id);


        return product;
    }

    public async Task<int> GetProductsCount()
    {
       return await _dbContext.Products.CountAsync(); 
    }

    public  async Task<Tuple<List<ProductResponseDto>, int>> SearchOnProductAsync(ProductSearchCriteria searchCriteria, int page , int count , List<SortingDefinition> sortingDefinitions)
    {
        var query = _dbContext.Products.AsQueryable();
        var TotalItems = 0;
        
        if (! string.IsNullOrEmpty(searchCriteria.ProductName))
        {

           query =  query.Where(x =>x.Name.Contains(searchCriteria.ProductName));
            TotalItems = query.Count();
        }
        if (!string.IsNullOrEmpty(searchCriteria.BrandName))
        {

           query =  query = query.Where(x => x.Brand.Name.Contains(searchCriteria.BrandName));
            TotalItems += query.Count();
        }
        if (!string.IsNullOrEmpty(searchCriteria.CategoryName))
        {

            query = query.Where(x => x.Category.Name.Contains(searchCriteria.CategoryName));
            TotalItems += query.Count();
        }
        if (!string.IsNullOrEmpty(searchCriteria.VendorName))
        {

           query = query.Where(x => x.Vendor.Name.Contains(searchCriteria.VendorName));
            TotalItems += query.Count();
        }
        if (!string.IsNullOrEmpty(searchCriteria.ProductDescription))
        {

            query = query.Where(x => x.Description.Contains(searchCriteria.ProductDescription));
            TotalItems += query.Count();
        }

        if (!string.IsNullOrEmpty(searchCriteria.SearchTerm))
        {
            query = query.Where(x =>
              x.Name.Contains(searchCriteria.SearchTerm) ||
              x.Description.Contains(searchCriteria.SearchTerm) ||
              x.Brand.Name.Contains(searchCriteria.SearchTerm) ||
              x.Category.Name.Contains(searchCriteria.SearchTerm) ||
              x.Vendor.Name.Contains(searchCriteria.SearchTerm)
              );
            TotalItems += query.Count();
        }

        var sortedProduct = await _sortingService.SortItems(query, sortingDefinitions);

        var products=await query.ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider)
             .Skip(page * count)
             .Take(count)
             .ToListAsync();

        return Tuple.Create(products, TotalItems);
    }

    public async Task<ProductResponseDto> UpdateProductAsync(Guid id, ProductRequestDto productRequestDto)
    {
        var product = await _dbContext.Products.FindAsync(id);

        _mapper.Map(productRequestDto, product);

        await _dbContext.SaveChangesAsync();

        var productResponse = _mapper.Map<ProductResponseDto>(product);

        return productResponse;
    }

}
