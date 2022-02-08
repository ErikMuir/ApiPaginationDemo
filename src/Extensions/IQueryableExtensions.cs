using System.Linq;
using ApiPaginationDemo.Models;

namespace ApiPaginationDemo.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paged<T>(this IQueryable<T> query, IPageRequest request)
        {
            var limit = request.PageSize;
            var offset = limit * (request.Page - 1);
            return query.Skip(offset).Take(limit);
        }
    }
}
