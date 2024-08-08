using Dashboard.Application.DTOS.EmployeeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Application.Sorting;

public class SortingService : ISortingService
{
    public async Task<List<T>> SortItems<T>(IEnumerable<T> items, ICollection<SortingDefinition> sortDefinitions)
    {
        if (sortDefinitions == null || sortDefinitions.Count == 0)
            return items.ToList();
        IOrderedEnumerable<T> orderedItems = null;

        foreach(var sortDefinition in sortDefinitions)
        {
            if (orderedItems is null)
            {
                orderedItems = sortDefinition.IsDescending
                    ? items.OrderByDescending(item => GetPropertyValue(item, sortDefinition.SortBy))
                    : items.OrderBy(item => GetPropertyValue(item, sortDefinition.SortBy));
            }
            else
            {
                orderedItems = sortDefinition.IsDescending
                    ? orderedItems.ThenByDescending(item => GetPropertyValue(item, sortDefinition.SortBy))
                    : orderedItems.ThenBy(item => GetPropertyValue(item, sortDefinition.SortBy));
            }
        }

        return orderedItems.ToList() ?? items.ToList();
    }

    private object GetPropertyValue<T>(T item, string propertyName) => typeof(T).GetProperty(propertyName).GetValue(item);

   
}
