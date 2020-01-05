using System;
using System.Collections.Generic;

namespace ApiPaginationDemo
{
    public class PageResponse<T>
    {
        private IPageRequest _request;

        public PageResponse(IPageRequest request, List<T> data, int totalCount)
        {
            _request = request;
            Data = data;
            TotalCount = totalCount;
        }

        public int CurrentPage => _request.Page;
        public int PageSize => _request.PageSize;
        public int PageCount
        {
            get
            {
                return PageSize == 0 ? 0 : (int)Math.Ceiling(TotalCount / (double)PageSize);
            }
        }
        public int TotalCount { get; private set; }
        public string Base => _request.Host;
        public string Self => GetPageUrl(CurrentPage);
        public string First => GetPageUrl(1);
        public string Last => GetPageUrl(PageCount);
        public string Prev => CurrentPage > 1 ? GetPageUrl(CurrentPage - 1) : null;
        public string Next => CurrentPage < PageCount ? GetPageUrl(CurrentPage + 1) : null;
        public List<T> Data { get; private set; }

        private string GetPageUrl(int page) => $"{_request.Path}?page={page}&pageSize={PageSize}";
    }
}
