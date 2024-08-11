using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.SortingCriteria.ProductSortingCriteria;
/// <summary>
/// Class that used to define sorting criteria of Columns in DataGrid
/// It assigned to int to define the sorting Type
/// 0 means that column has no sorting criteria
/// 1 means that column is sorted in ascending order
/// 2 means that column is sorted in descending order
/// </summary>
public class ProductSortingCriteria
{
    public int ProductName { get; set; } = 0;
    public int BrandName { get; set; }
    public int ProductDescription { get; set; }
    public int CategoryName { get; set; }
    public int VendorName { get; set; }
}
