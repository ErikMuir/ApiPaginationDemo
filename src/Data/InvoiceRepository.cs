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
        PageResponse<Invoice> GetInvoices_RobustPagination(GetInvoicesRequest request);
    }

    public class InvoiceRepository : IInvoiceRepository
    {
        private static IQueryable<Invoice> _invoices;

        static InvoiceRepository()
        {
            if (_invoices == null)
            {
                var json = File.ReadAllText("Data/invoices.json");
                _invoices = JsonSerializer.Deserialize<List<Invoice>>(json).AsQueryable();
            }
        }

        public List<Invoice> GetInvoices_NoPagination(Guid customerId)
        {
            return _invoices
                .Where(x => x.CustomerId == customerId)
                .ToList();
        }

        public List<Invoice> GetInvoices_QuickAndDirtyPagination(Guid customerId, int page, int pageSize)
        {
            var offset = pageSize * (page - 1);
            return _invoices
                .Where(x => x.CustomerId == customerId)
                .Skip(offset)
                .Take(pageSize)
                .ToList();

        }

        public PageResponse<Invoice> GetInvoices_RobustPagination(GetInvoicesRequest request)
        {
            var query = _invoices.Where(x => x.CustomerId == request.CustomerId);

            var totalCount = query.Count();
            var data = query.Paged(request).ToList();

            return new PageResponse<Invoice>(request, data, totalCount);
        }
    }
}