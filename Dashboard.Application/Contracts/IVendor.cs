using Dashboard.Application.DTOS.VendorDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.Contracts;

public interface IVendorService
{
    /// <summary>
    /// Asynchronously adds a new vendor.
    /// </summary>
    /// <param name="vendorRequestDto">The data transfer object containing vendor information.</param>
    /// <returns>A task representing the result of adding the vendor successfully.</returns>
    public Task AddVendorAsync(List<VendorRequestDto> vendorRequestDto);

    /// <summary>
    /// Asynchronously retrieves all vendors.
    /// </summary>
    /// <returns>A task representing the result of retrieving a list of vendor response data transfer objects.</returns>
    public Task<List<VendorResponseDto>> GetAllVendorsAsync();

    /// <summary>
    /// Asynchronously retrieves a vendor by its ID.
    /// </summary>
    /// <param name="id">The ID of the vendor to retrieve.</param>
    /// <returns>A task representing the result of retrieving the vendor response data transfer object.</returns>
    public Task<VendorResponseDto> GetVendorByIdAsync(Guid id);

    /// <summary>
    /// Asynchronously updates a vendor by its ID.
    /// </summary>
    /// <param name="id">The ID of the vendor to update.</param>
    /// <param name="vendorRequestDto">The data transfer object containing updated vendor information.</param>
    /// <returns>A task representing the result of updating the vendor response data transfer object.</returns>
    public Task<VendorResponseDto> UpdateVendorAsync(Guid id, VendorRequestDto vendorRequestDto);

    /// <summary>
    /// Asynchronously deletes a vendor by its ID.
    /// </summary>
    /// <param name="id">The ID of the vendor to delete.</param>
    /// <returns>A task representing the result of deleting the vendor successfully.</returns>
    public Task DeleteVendorAsync(Guid id);
}
