using Dashboard.Application.DTOS.CategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.Contracts;

public interface ICategoryService
{
    /// <summary>
    /// Asynchronously adds a new category.
    /// </summary>
    /// <param name="categoryRequestDto">The data transfer object containing category information.</param>
    /// <returns>A task representing the result of adding the category successfully.</returns>
    public Task AddCategoryAsync(List<CategoryRequestDto> categoryRequestDto);

    /// <summary>
    /// Asynchronously retrieves all categorys.
    /// </summary>
    /// <returns>A task representing the result of retrieving a list of category response data transfer objects.</returns>
    public Task<List<CategoryResponseDto>> GetAllCategorysAsync();

    /// <summary>
    /// Asynchronously retrieves a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to retrieve.</param>
    /// <returns>A task representing the result of retrieving the category response data transfer object.</returns>
    public Task<CategoryResponseDto> GetCategoryByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously updates a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to update.</param>
    /// <param name="categoryRequestDto">The data transfer object containing updated category information.</param>
    /// <returns>A task representing the result of updating the category response data transfer object.</returns>
    public Task<CategoryResponseDto> UpdateCategoryAsync(Guid id, CategoryRequestDto categoryRequestDto);

    /// <summary>
    /// Asynchronously deletes a category by its ID.
    /// </summary>
    /// <param name="id">The ID of the category to delete.</param>
    /// <returns>A task representing the result of deleting the category successfully.</returns>
    public Task DeleteCategoryAsync(Guid id);
}
