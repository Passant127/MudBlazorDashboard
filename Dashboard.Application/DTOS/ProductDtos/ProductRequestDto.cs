using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.DTOS.ProductDtos;

public class ProductRequestDto
{
    public string Name { get; set; }

    public string Description { get; set; }


    public int Price { get; set; }

    public Guid VendorId { get; set; }
    public Guid CategoryId { get; set; }
    public Guid BrandId { get; set; }

}
