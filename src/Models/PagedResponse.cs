using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ApiPaginationDemo.Models;

public class PagedResponse<T>
{
    private readonly string _path;

    public PagedResponse(IPageRequest pageRequest, HttpRequest httpRequest, PagedResult<T> pagedResult)
    {
        CurrentPage = pageRequest.Page;
        PageSize = pageRequest.PageSize;
        TotalCount = pagedResult.TotalCount;
        Data = pagedResult.Data;
        PageCount = PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
        Base = httpRequest.Host.ToUriComponent();
        _path = httpRequest.Path.ToUriComponent();
    }

    public int CurrentPage { get; }
    public int PageSize { get; }
    public int PageCount { get; }
    public int TotalCount { get; }
    public List<T> Data { get; }
    public string Base { get; }
    public string Self => GetPageUrl(CurrentPage);
    public string First => GetPageUrl(1);
    public string Last => PageCount == 1 ? null : GetPageUrl(PageCount);
    public string Prev => CurrentPage > 1 ? GetPageUrl(CurrentPage - 1) : null;
    public string Next => CurrentPage < PageCount ? GetPageUrl(CurrentPage + 1) : null;

    private string GetPageUrl(int page) => $"{_path}?page={page}&pageSize={PageSize}";
}
