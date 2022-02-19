using System.Collections.Generic;

namespace ApiPaginationDemo.Models;

public class PagedResult<T>
{
    public PagedResult(int totalCount, List<T> data)
    {
        TotalCount = totalCount;
        Data = data ?? new List<T>();
    }

    public int TotalCount { get; }
    public List<T> Data { get; }
}
