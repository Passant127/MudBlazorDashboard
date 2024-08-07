using Dashboard.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Domain.Entities;

public class Brand : BaseEntity
{

    public string Name { get; set; }
    public virtual ICollection<Product> Products { get; set; }

}
