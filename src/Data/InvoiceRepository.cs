using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ApiPaginationDemo
{
    public interface IInvoiceRepository
    {
        List<Invoice> GetInvoices_NoPagination(Guid customerId);
        List<Invoice> GetInvoices_QuickAndDirtyPagination(Guid customerId, int page, int pageSize);
        List<Invoice> GetInvoices_RobustPagination(GetInvoicesRequestModel requestModel, out int totalCount);
    }

    public class InvoiceRepository : IInvoiceRepository
    {
        private static JsonDbContext _dbContext;

        static InvoiceRepository()
        {
            if (_dbContext == null)
            {
                var json = File.ReadAllText("Data/invoices.json");
                var invoices = JsonSerializer.Deserialize<List<Invoice>>(json).AsQueryable();
                _dbContext = new JsonDbContext { Invoices = invoices };
            }
        }

        public List<Invoice> GetInvoices_NoPagination(Guid customerId)
        {
            return _dbContext.Invoices
                .Where(x => x.CustomerId == customerId)
                .ToList();
        }

        public List<Invoice> GetInvoices_QuickAndDirtyPagination(Guid customerId, int page, int pageSize)
        {
            var offset = pageSize * (page - 1);
            return _dbContext.Invoices
                .Where(x => x.CustomerId == customerId)
                .Skip(offset)
                .Take(pageSize)
                .ToList();
        }

        public List<Invoice> GetInvoices_RobustPagination(GetInvoicesRequestModel requestModel, out int totalCount)
        {
            var query = _dbContext.Invoices.Where(x => x.CustomerId == requestModel.CustomerId);
            totalCount = query.Count();
            return query.Paged(requestModel).ToList();
        }
    }
}