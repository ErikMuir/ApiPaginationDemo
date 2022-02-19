using System;
using System.Collections.Generic;
using ApiPaginationDemo.Data;
using ApiPaginationDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiPaginationDemo.Controllers.V2;

[ApiVersion("2")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceRepository _invoiceRepository;

    public InvoicesController(IInvoiceRepository invoiceRepository)
    {
        _invoiceRepository = invoiceRepository;
    }

    [HttpGet("{customerId}")]
    public ActionResult<List<Invoice>> Get(Guid customerId, int page, int pageSize)
    {
        return _invoiceRepository.Get(customerId, page, pageSize);
    }
}
