using Dashboard.Application.DTOS.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.Contracts;

public interface IProduct
{
    /// <summary>
    /// Asynchronously adds a new product.
    /// </summary>
    /// <param name="productRequestDto">The data transfer object containing product information.</param>
    /// <returns>A task representing the result of adding the product successfully.</returns>
    public Task AddProductAsync(List<ProductRequestDto> productRequestDto);

    /// <summary>
    /// Asynchronously retrieves all products.
    /// </summary>
    /// <returns>A task representing the result of retrieving a list of product response data transfer objects.</returns>
    public Task<List<ProductResponseDto>> GetAllProductsAsync();

    /// <summary>
    /// Asynchronously retrieves a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to retrieve.</param>
    /// <returns>A task representing the result of retrieving the product response data transfer object.</returns>
    public Task<ProductResponseDto> GetProductByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously updates a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to update.</param>
    /// <param name="productRequestDto">The data transfer object containing updated product information.</param>
    /// <returns>A task representing the result of updating the product response data transfer object.</returns>
    public Task<ProductResponseDto> UpdateProductAsync(Guid id, ProductRequestDto productRequestDto);

    /// <summary>
    /// Asynchronously deletes a product by its ID.
    /// </summary>
    /// <param name="id">The ID of the product to delete.</param>
    /// <returns>A task representing the result of deleting the product successfully.</returns>
    public Task DeleteProductAsync(Guid id);
}
