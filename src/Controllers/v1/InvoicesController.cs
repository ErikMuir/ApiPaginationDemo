using System;
using System.Collections.Generic;
using ApiPaginationDemo.Data;
using ApiPaginationDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiPaginationDemo.Controllers.V1
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
        public ActionResult<List<Invoice>> Get(Guid customerId)
        {
            return _invoiceRepository.Get(customerId);
        }
    }
}
