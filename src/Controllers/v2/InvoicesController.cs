using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ApiPaginationDemo.Controllers.v2
{
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
        public List<Invoice> Get_QuickAndDirtyPagination(Guid customerId, int page, int pageSize)
        {
            return _invoiceRepository.GetInvoices_QuickAndDirtyPagination(customerId, page, pageSize);
        }
    }
}
