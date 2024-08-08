using Dashboard.Application.DTOS.ProductDtos;
using Dashboard.Application.SearchingCriteria.ProductSearchCriteria;


namespace Dashboard.Application.Contracts;

public interface IProductService
{
    /// <summary>
    /// Asynchronously adds a new product.
    /// </summary>
    /// <param name="productRequestDto">The data transfer object containing product information.</param>
    /// <returns>A task representing the result of adding the product successfully.</returns>
     Task AddProductAsync(List<ProductRequestDto> productRequestDto);

    /// <summary>
    /// Asynchronously retrieves all products.
    /// </summary>
    /// <returns>A task representing the result of retrieving a list of product response data transfer objects.</returns>
     Task<List<ProductResponseDto>> GetAllProductsAsync();

    /// <summary>
    /// Asynchronously retrieves pages of products.
    /// </summary>
    /// <returns>A task representing the result of retrieving a list of product depends on page.</returns>
     Task<List<ProductResponseDto>> GetAllProductsAsync(int page, int count);

    /// <summary>
    /// Asynchronously retrieves a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <returns>A task representing the result of retrieving the product response data transfer object.</returns>
    Task<ProductResponseDto> GetProductByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously updates a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="productRequestDto">The data transfer object containing updated product information.</param>
    /// <returns>A task representing the result of updating the product response data transfer object.</returns>
    Task<ProductResponseDto> UpdateProductAsync(Guid id, ProductRequestDto productRequestDto);

    /// <summary>
    /// Asynchronously deletes a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <returns>A task representing the result of deleting the product successfully.</returns>
    Task DeleteProductAsync(Guid id);

    /// <summary>
    /// Asynchronously searches on all products by specific word.
    /// </summary>
    /// <param searchitem="text".A text in product name or description or brand name or category name or vendor name"</param>
    /// <returns>A task representing the result of getting list of products successfully.</returns>
    Task <Tuple< List<ProductResponseDto> ,int >> SearchOnProductAsync(ProductSearchCriteria searchitem, int page = 0, int count = 100);

    /// <summary>
    /// Asynchronously get products count.
    /// </summary>
    /// <returns>A task representing the result of retrieving count product .</returns>
     Task<int> GetProductsCount();


}
