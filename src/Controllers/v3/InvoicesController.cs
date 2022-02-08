using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ApiPaginationDemo.Controllers.v3
{
    [ApiVersion("3")]
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
        public PageResponse<Invoice> Get_RobustPagination([FromQuery] GetInvoicesRequestModel requestModel)
        {
            var data = _invoiceRepository.GetInvoices_RobustPagination(requestModel);
            return new PageResponse<Invoice>(requestModel, Request, data.TotalCount, data.Invoices);
        }
    }
}
