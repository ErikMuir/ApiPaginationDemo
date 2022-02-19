using System;
using System.ComponentModel.DataAnnotations;
using ApiPaginationDemo.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ApiPaginationDemo.Models;

public class GetInvoicesRequestModel : IPageRequest
{
    public GetInvoicesRequestModel()
    {
        Page = Defaults.Page;
        PageSize = Defaults.PageSize;
    }

    [FromRoute]
    public Guid CustomerId { get; set; }

    [Range(1, Int32.MaxValue)]
    public int Page { get; set; }

    [Range(1, Defaults.MaxPageSize)]
    public int PageSize { get; set; }
}
