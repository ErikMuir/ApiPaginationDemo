using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace ApiPaginationDemo
{
    public class PageResponse<T>
    {
        private string _path;

        public PageResponse(IPageRequest pageRequest, HttpRequest httpRequest, List<T> data, int totalCount)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = pageRequest.Page;
            PageSize = pageRequest.PageSize;
            Base = httpRequest.Host.ToUriComponent();
            _path = httpRequest.Path.ToUriComponent();
        }

        public int CurrentPage { get; }
        public int PageSize { get; }
        public int PageCount
        {
            get
            {
                return PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
            }
        }
        public int TotalCount { get; }
        public string Base { get; }
        public string Self => GetPageUrl(CurrentPage);
        public string First => GetPageUrl(1);
        public string Last => GetPageUrl(PageCount);
        public string Prev => CurrentPage > 1 ? GetPageUrl(CurrentPage - 1) : null;
        public string Next => CurrentPage < PageCount ? GetPageUrl(CurrentPage + 1) : null;
        public List<T> Data { get; private set; }

        private string GetPageUrl(int page) => $"{_path}?page={page}&pageSize={PageSize}";
    }
}
