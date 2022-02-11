using System.Collections.Generic;

namespace ApiPaginationDemo.Models
{
    public class PagedResult<T>
    {
        public PagedResult(int totalCount, List<T> items)
        {
            TotalCount = totalCount;
            Items = items;
        }

        public int TotalCount { get; }
        public List<T> Items { get; }
    }
}
