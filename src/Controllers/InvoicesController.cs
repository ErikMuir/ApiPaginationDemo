using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ApiPaginationDemo
{
    [ApiVersion("1")]
    [ApiVersion("2")]
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

        [MapToApiVersion("1")]
        [HttpGet("{customerId}")]
        public List<Invoice> Get_NoPagination(Guid customerId)
        {
            return _invoiceRepository.GetInvoices_NoPagination(customerId);
        }

        [MapToApiVersion("2")]
        [HttpGet("{customerId}")]
        public List<Invoice> Get_QuickAndDirtyPagination(Guid customerId, int page, int pageSize)
        {
            return _invoiceRepository.GetInvoices_QuickAndDirtyPagination(customerId, page, pageSize);
        }

        [MapToApiVersion("3")]
        [HttpGet("{customerId}")]
        public PageResponse<Invoice> Get_RobustPagination([FromQuery] GetInvoicesRequestModel requestModel)
        {
            var data = _invoiceRepository.GetInvoices_RobustPagination(requestModel);
            return new PageResponse<Invoice>(requestModel, Request, data.TotalCount, data.Invoices);
        }
    }
}
