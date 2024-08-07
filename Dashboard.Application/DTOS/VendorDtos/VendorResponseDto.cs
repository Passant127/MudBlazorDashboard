using Dashboard.Application.DTOS.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.DTOS.VendorDtos;

public class VendorResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }

    public ICollection<ProductResponseDto>  Products { get; set; }
}
