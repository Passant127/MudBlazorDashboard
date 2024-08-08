using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dashboard.Application.Contracts;
using Dashboard.Application.DTOS.ProductDtos;
using Dashboard.Application.SearchingCriteria.ProductSearchCriteria;
using Dashboard.Domain.Entities;
using Dashboard.Infrastrcuture.BaseContext;
using Microsoft.EntityFrameworkCore;


namespace Dashboard.Application.Services;

public class ProductService(DashboardDbContext dbContext, IMapper mapper) : IProductService
{
    private readonly DashboardDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

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

    public async Task<List<ProductResponseDto>> GetAllProductsAsync()
    {
        var products = await _dbContext.Products.ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider).ToListAsync();

        return products;
    }

    public async Task<List<ProductResponseDto>> GetAllProductsAsync(int page=0, int count=10)
    {
        var products = await _dbContext.Products.ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider).Skip(page*count)       
            .Take(count)
            .ToListAsync();
        return products ;

    }

    public async Task<ProductResponseDto> GetProductByIdAsync(Guid id)
    {
        var product = await _dbContext.Products
                 .ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(s => s.Id == id);


        return product;
    }
    
    public async Task<List<ProductResponseDto>> SearchOnProductAsync(ProductSearchCriteria searchCriteria)
    {
        var query = _dbContext.Products.AsQueryable();
        
        if (! string.IsNullOrEmpty(searchCriteria.ProductName))
        {

            query.Where(x =>x.Name.Contains(searchCriteria.ProductName));
        }
        if (!string.IsNullOrEmpty(searchCriteria.BrandName))
        {

            query.Where(x => x.Brand.Name.Contains(searchCriteria.BrandName));
        }
        if (!string.IsNullOrEmpty(searchCriteria.CategoryName))
        {

            query.Where(x => x.Category.Name.Contains(searchCriteria.CategoryName));
        }
        if (!string.IsNullOrEmpty(searchCriteria.VendorName))
        {

            query.Where(x => x.Vendor.Name.Contains(searchCriteria.VendorName));
        }
        if (!string.IsNullOrEmpty(searchCriteria.ProductDescription))
        {

            query.Where(x => x.Description.Contains(searchCriteria.ProductDescription));
        }

        if (!string.IsNullOrEmpty(searchCriteria.SearchTerm))
        {
            query.Where(x =>
              x.Name.Contains(searchCriteria.SearchTerm) ||
              x.Description.Contains(searchCriteria.SearchTerm) ||
              x.Brand.Name.Contains(searchCriteria.SearchTerm) ||
              x.Category.Name.Contains(searchCriteria.SearchTerm) ||
              x.Vendor.Name.Contains(searchCriteria.SearchTerm)
              );
        }


        try
        {
            var result = await query.ToListAsync();

            Console.WriteLine(result); // Log or inspect the exception details
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message); // Log or inspect the exception details
            throw;
        }

        return  await query.ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider).ToListAsync();
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
