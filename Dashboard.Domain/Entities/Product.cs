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
    public float Price { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; } = new Category();
    public Guid BrandId { get; set; }
    public virtual Brand Brand { get; set; } = new Brand();
}
