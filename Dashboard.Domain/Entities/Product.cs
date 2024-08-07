using Dashboard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; } 
    public Guid BrandId { get; set; }
    public virtual Brand Brand { get; set; }
    public Guid VendorId { get; set; }
    public virtual Vendor Vendor { get; set; } 

}
