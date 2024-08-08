using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dashboard.Application.Contracts;
using Dashboard.Application.DTOS.ProductDtos;
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
        var products = await _dbContext.Products.ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider)
            .Skip(page*count)
            .Take(count)
            .ToListAsync();

        return products;
    }

    public async Task<ProductResponseDto> GetProductByIdAsync(Guid id)
    {
        var product = await _dbContext.Products
                 .ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider)
                 .FirstOrDefaultAsync(s => s.Id == id);


        return product;
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
