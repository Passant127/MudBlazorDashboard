using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.Sorting;

public interface ISortingService
{
    Task<List<T>> SortItems<T>(IEnumerable<T> items, ICollection<SortingDefinition> sortDefinitions);
}
