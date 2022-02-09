using ApiPaginationDemo.Data;
using ApiPaginationDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiPaginationDemo.Controllers.V3
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
        public ActionResult<PagedResponse<Invoice>> Get([FromQuery] GetInvoicesRequestModel requestModel)
        {
            var data = _invoiceRepository.Get(requestModel);
            return new PagedResponse<Invoice>(requestModel, Request, data.TotalCount, data.Invoices);
        }
    }
}
