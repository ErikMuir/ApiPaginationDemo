using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ApiPaginationDemo.Controllers.v1
{
    [ApiVersion("1")]
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
        public List<Invoice> Get(Guid customerId)
        {
            return _invoiceRepository.GetInvoices_NoPagination(customerId);
        }
    }
}
