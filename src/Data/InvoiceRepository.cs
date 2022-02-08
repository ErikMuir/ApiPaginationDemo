using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ApiPaginationDemo.Extensions;
using ApiPaginationDemo.Models;

namespace ApiPaginationDemo.Data
{
    public interface IInvoiceRepository
    {
        List<Invoice> Get_NoPagination(Guid customerId);
        List<Invoice> Get_QuickAndDirtyPagination(Guid customerId, int page, int pageSize);
        (int TotalCount, List<Invoice> Invoices) Get_RobustPagination(GetInvoicesRequestModel requestModel);
    }

    public class InvoiceRepository : IInvoiceRepository
    {
        private const string JsonDataPath = @"Data/invoices.json";
        private static JsonDbContext _dbContext;
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        static InvoiceRepository()
        {
            if (_dbContext == null)
            {
                var json = File.ReadAllText(JsonDataPath);
                var invoices = JsonSerializer.Deserialize<List<Invoice>>(json, _options).AsQueryable();
                _dbContext = new JsonDbContext { Invoices = invoices };
            }
        }

        public List<Invoice> Get_NoPagination(Guid customerId)
        {
            return _dbContext.Invoices
                .Where(x => x.CustomerId == customerId)
                .ToList();
        }

        public List<Invoice> Get_QuickAndDirtyPagination(Guid customerId, int page, int pageSize)
        {
            var offset = pageSize * (page - 1);
            return _dbContext.Invoices
                .Where(x => x.CustomerId == customerId)
                .Skip(offset)
                .Take(pageSize)
                .ToList();
        }

        public (int TotalCount, List<Invoice> Invoices) Get_RobustPagination(GetInvoicesRequestModel requestModel)
        {
            var baseQuery = _dbContext.Invoices.Where(x => x.CustomerId == requestModel.CustomerId);
            return (baseQuery.Count(), baseQuery.Paged(requestModel).ToList());
        }
    }
}