using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ApiPaginationDemo.Models
{
    public class GetInvoicesRequestModel : IPageRequest
    {
        private const int _defaultPage = 1;
        private const int _defaultPageSize = 10;
        private const int _maxPageSize = 50;

        public GetInvoicesRequestModel()
        {
            Page = _defaultPage;
            PageSize = _defaultPageSize;
        }

        [FromRoute]
        public Guid CustomerId { get; set; }

        [Range(1, Int32.MaxValue)]
        public int Page { get; set; }

        [Range(1, _maxPageSize)]
        public int PageSize { get; set; }
    }
}
