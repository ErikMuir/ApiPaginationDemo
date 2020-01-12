using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ApiPaginationDemo
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
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
        public List<Invoice> Get_NoPagination(Guid customerId)
        {
            return _invoiceRepository.GetInvoices_NoPagination(customerId);
        }

        [MapToApiVersion("2.0")]
        [HttpGet("{customerId}")]
        public List<Invoice> Get_QuickAndDirtyPagination(Guid customerId, int page, int pageSize)
        {
            return _invoiceRepository.GetInvoices_QuickAndDirtyPagination(customerId, page, pageSize);
        }

        [MapToApiVersion("3.0")]
        [HttpGet("{customerId}")]
        public PageResponse<Invoice> Get_RobustPagination([FromQuery] GetInvoicesRequest request)
        {
            var data = _invoiceRepository.GetInvoices_RobustPagination(request, out int totalCount);
            return new PageResponse<Invoice>(request, Request, data, totalCount);
        }
    }
}
