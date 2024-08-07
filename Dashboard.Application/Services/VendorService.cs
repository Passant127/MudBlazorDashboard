using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dashboard.Application.Contracts;
using Dashboard.Application.DTOS.VendorDtos;
using Dashboard.Domain.Entities;
using Dashboard.Infrastrcuture.BaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.Services;


public class VendorService(DashboardDbContext dbContext, IMapper mapper) : IVendorService
{
    private readonly DashboardDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;


    public async Task AddVendorAsync(List<VendorRequestDto> vendorRequestDto)
    {
        var mappedVendor = _mapper.Map<List<Vendor>>(vendorRequestDto);
        await _dbContext.AddRangeAsync(mappedVendor);

        await _dbContext.SaveChangesAsync();


    }

    public async Task DeleteVendorAsync(Guid id)
    {
        var vendor = await _dbContext.Vendors.FindAsync(id);
        _dbContext.Vendors.Remove(vendor);
        await _dbContext.SaveChangesAsync();


    }

    public async Task<List<VendorResponseDto>> GetAllVendorsAsync()
    {
        var vendors = await _dbContext.Vendors.ProjectTo<VendorResponseDto>(_mapper.ConfigurationProvider).ToListAsync();

        return vendors;
    }

    public async Task<VendorResponseDto> GetVendorByIdAsync(Guid id)
    {
        var vendor = await _dbContext.Vendors
         .ProjectTo<VendorResponseDto>(_mapper.ConfigurationProvider)
         .FirstOrDefaultAsync(s => s.Id == id);


        return vendor;
    }

    public async Task<VendorResponseDto> UpdateVendorAsync(Guid id, VendorRequestDto vendorRequestDto)
    {
        var vendor = await _dbContext.Vendors.FindAsync(id);



        _mapper.Map(vendorRequestDto, vendor);

        await _dbContext.SaveChangesAsync();

        var vendorResponse = _mapper.Map<VendorResponseDto>(vendor);




        return vendorResponse;
    }
}
