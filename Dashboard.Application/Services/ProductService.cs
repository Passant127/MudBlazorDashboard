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
        bool flag = false;
        if (productSortingCriteria.ProductName != 0)
        {
            query = (productSortingCriteria.ProductName == 1 ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name));
            flag = true;
        }
        if (productSortingCriteria.ProductDescription != 0)
        {
            query = flag ==true?
                (productSortingCriteria.ProductDescription == 1? ((IOrderedQueryable<Product>)query).ThenBy(x => x.Description) : ((IOrderedQueryable<Product>)query).ThenByDescending(x => x.Description)) :
             (productSortingCriteria.ProductDescription == 1 ? query.OrderBy(x => x.Description) : query.OrderByDescending(x => x.Description));
            flag = true;
        }
        if (productSortingCriteria.CategoryName != 0)
        {
            query = flag == true ?
                (productSortingCriteria.CategoryName == 1 ? ((IOrderedQueryable<Product>)query).ThenBy(x => x.Category.Name) : ((IOrderedQueryable<Product>)query).ThenByDescending(x => x.Category.Name)) :
             (productSortingCriteria.CategoryName == 1 ? query.OrderBy(x => x.Category.Name) : query.OrderByDescending(x => x.Category.Name));
            flag = true;
        }
        if (productSortingCriteria.BrandName != 0)
        {

            query = flag == true ?
                (productSortingCriteria.BrandName == 1 ? ((IOrderedQueryable<Product>)query).ThenBy(x => x.Brand.Name) : ((IOrderedQueryable<Product>)query).ThenByDescending(x => x.Brand.Name)) :
             (productSortingCriteria.BrandName == 1 ? query.OrderBy(x => x.Brand.Name) : query.OrderByDescending(x => x.Brand.Name));
            flag = true;
        }
        if (productSortingCriteria.VendorName != 0)
        {

            query = flag == true ?
                (productSortingCriteria.VendorName == 1 ? ((IOrderedQueryable<Product>)query).ThenBy(x => x.Vendor.Name) : ((IOrderedQueryable<Product>)query).ThenByDescending(x => x.Vendor.Name)) :
             (productSortingCriteria.VendorName == 1 ? query.OrderBy(x => x.Vendor.Name) : query.OrderByDescending(x => x.Vendor.Name));
        }
        if (productSortingCriteria.Price != 0)
        {

            query = flag == true ?
                (productSortingCriteria.Price == 1 ? ((IOrderedQueryable<Product>)query).ThenBy(x => x.Price) : ((IOrderedQueryable<Product>)query).ThenByDescending(x => x.Price)) :
             (productSortingCriteria.Price == 1 ? query.OrderBy(x => x.Price) : query.OrderByDescending(x => x.Price));
        }
        
        return  query;
    }

    public  async Task<Tuple<List<ProductResponseDto>, int>> SearchOnProductAsync(ProductSearchCriteria searchCriteria, int page , int count , ProductSortingCriteria productSortingCriteria)
    {
        var query = await GetSortingCriteria(productSortingCriteria);
        var TotalItems = 0;
        
        if (! string.IsNullOrEmpty(searchCriteria.ProductName))
        {

           query =  query.Where(x =>x.Name.Contains(searchCriteria.ProductName));
            TotalItems = query.Count();
        }
        if (!string.IsNullOrEmpty(searchCriteria.BrandName))
        {

           query = query.Where(x => x.Brand.Name.Contains(searchCriteria.BrandName));
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


        var products=await query.ProjectTo<ProductResponseDto>(_mapper.ConfigurationProvider)
             .Skip(page * count)
             .Take(count)
             .ToListAsync();

        return Tuple.Create(products, query.Count());
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
