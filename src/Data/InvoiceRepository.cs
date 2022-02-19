using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ApiPaginationDemo.Helpers;
using ApiPaginationDemo.Models;

namespace ApiPaginationDemo.Data;

public interface IInvoiceRepository
{
    List<Invoice> Get(Guid customerId);
    List<Invoice> Get(Guid customerId, int page, int pageSize);
    PagedResult<Invoice> Get(GetInvoicesRequestModel requestModel);
}

public class InvoiceRepository : IInvoiceRepository
{
    private const string JsonDataPath = @"Data/invoices.json";
    private static JsonDbContext _dbContext;
    private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
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

    // no pagination
    public List<Invoice> Get(Guid customerId)
    {
        return _dbContext.Invoices
            .Where(x => x.CustomerId == customerId)
            .ToList();
    }

    // quick and dirty pagination
    public List<Invoice> Get(Guid customerId, int page, int pageSize)
    {
        var offset = pageSize * (page - 1);
        return _dbContext.Invoices
            .Where(x => x.CustomerId == customerId)
            .Skip(offset)
            .Take(pageSize)
            .ToList();
    }

    // accomodating pagination
    public PagedResult<Invoice> Get(GetInvoicesRequestModel model)
    {
        var baseQuery = _dbContext.Invoices.Where(x => x.CustomerId == model.CustomerId);
        var totalCount = baseQuery.Count();
        var invoices = baseQuery.Paged(model).ToList();
        return new PagedResult<Invoice>(totalCount, invoices);
    }
}
