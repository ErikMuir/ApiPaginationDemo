namespace ApiPaginationDemo.Models
{
    public interface IPageRequest
    {
        int Page { get; set; }
        int PageSize { get; set; }
    }
}
