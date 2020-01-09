using System;

namespace ApiPaginationDemo
{
    public class GetInvoicesRequest : IPageRequest
    {
        public Guid CustomerId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
    }
}
