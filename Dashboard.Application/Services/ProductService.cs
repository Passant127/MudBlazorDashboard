using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dashboard.Application.Contracts;
using Dashboard.Application.DTOS.ProductDtos;
using Dashboard.Application.SearchingCriteria.ProductSearchCriteria;
using Dashboard.Application.SortingCriteria.ProductSortingCriteria;
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

    public async Task<List<ProductResponseDto>> GetAllProductsAsync(int page, int count , ProductSortingCriteria productSortingCriteria)
    {
        var query=_dbContext.Products.AsQueryable(); 
        if (productSortingCriteria != null)
        {
            query = await GetSortingCriteria(productSortingCriteria);
        }
        var products = await query.ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider)
            .Skip(page*count)       
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

    public async Task<int> GetProductsCount()
    {
       return await _dbContext.Products.CountAsync(); 
    }

    public async Task<IQueryable<Product>> GetSortingCriteria(ProductSortingCriteria productSortingCriteria)
    {
        var query = _dbContext.Products.AsQueryable();
        if (productSortingCriteria.ProductName != 0)
        {
            query = (productSortingCriteria.ProductName == 1 ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name));
        }
        if (productSortingCriteria.ProductDescription != 0)
        {
            query = (productSortingCriteria.ProductDescription == 1 ? query.OrderBy(x => x.Description) : query.OrderByDescending(x => x.Description));
        }
        if (productSortingCriteria.CategoryName != 0)
        {
            query = (productSortingCriteria.CategoryName == 1 ? query.OrderBy(x => x.Category.Name) : query.OrderByDescending(x => x.Category.Name));
        }
        if (productSortingCriteria.BrandName != 0)
        {
            query = (productSortingCriteria.BrandName == 1 ? query.OrderBy(x => x.Brand.Name) : query.OrderByDescending(x => x.Brand.Name));
        }
        if (productSortingCriteria.VendorName != 0)
        {
            query = (productSortingCriteria.VendorName == 1 ? query.OrderBy(x => x.Vendor.Name) : query.OrderByDescending(x => x.Vendor.Name));
        }
        
        return  query;
    }

    public  async Task<Tuple<List<ProductResponseDto>, int>> SearchOnProductAsync(ProductSearchCriteria searchCriteria, int page , int count , ProductSortingCriteria productSortingCriteria)
    {
        var query = _dbContext.Products.AsQueryable();
        var queryOfSorting = await GetSortingCriteria(productSortingCriteria);
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

        var combinedquery = query.Union(queryOfSorting);


        var products=await combinedquery.ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider)
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
