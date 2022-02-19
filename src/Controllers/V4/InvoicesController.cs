using ApiPaginationDemo.Data;
using ApiPaginationDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiPaginationDemo.Controllers.V4;

[ApiVersion("4")]
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
    public ActionResult<PagedResponse<Invoice>> Get([FromQuery] GetInvoicesRequestModel model)
    {
        var result = _invoiceRepository.Get(model);
        return new PagedResponse<Invoice>(model, Request, result);
    }
}
