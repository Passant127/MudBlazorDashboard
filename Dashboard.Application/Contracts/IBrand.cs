using Dashboard.Application.DTOS.Brands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.Contracts;

public interface IBrandService
{
    /// <summary>
    /// Asynchronously adds a new brand.
    /// </summary>
    /// <param name="brandRequestDto">The data transfer object containing brand information.</param>
    /// <returns>A task representing the result of adding the brand successfully.</returns>
    public Task AddBrandAsync(List<BrandRequestDto> brandRequestDto);

    /// <summary>
    /// Asynchronously retrieves all brands.
    /// </summary>
    /// <returns>A task representing the result of retrieving a list of brand response data transfer objects.</returns>
    public Task<List<BrandResponseDto>> GetAllBrandsAsync();

    /// <summary>
    /// Asynchronously retrieves a brand by its ID.
    /// </summary>
    /// <param name="id">The ID of the brand to retrieve.</param>
    /// <returns>A task representing the result of retrieving the brand response data transfer object.</returns>
    public Task<BrandResponseDto> GetBrandByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously updates a brand by its ID.
    /// </summary>
    /// <param name="id">The ID of the brand to update.</param>
    /// <param name="brandRequestDto">The data transfer object containing updated brand information.</param>
    /// <returns>A task representing the result of updating the brand response data transfer object.</returns>
    public Task<BrandResponseDto> UpdateBrandAsync(Guid id, BrandRequestDto brandRequestDto);

    /// <summary>
    /// Asynchronously deletes a brand by its ID.
    /// </summary>
    /// <param name="id">The ID of the brand to delete.</param>
    /// <returns>A task representing the result of deleting the brand successfully.</returns>
    public Task DeleteBrandAsync(Guid id);
}
