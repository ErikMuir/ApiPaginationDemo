namespace ApiPaginationDemo
{
    public interface IPageRequest
    {
        int Page { get; set; }
        int PageSize { get; set; }
        string Host { get; set; }
        string Path { get; set; }
    }
}
