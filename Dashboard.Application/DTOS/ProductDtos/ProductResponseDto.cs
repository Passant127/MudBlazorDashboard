using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.DTOS.ProductDtos;

public class ProductResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public string CategoryName { get; set; }
    public string BrandName { get; set; }

    public Guid CategoryId { get; set; }
    public Guid BrandId { get; set; }
}
