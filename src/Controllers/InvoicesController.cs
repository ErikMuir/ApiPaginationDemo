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
        private readonly IInvoiceRepository _studentRepository;

        public InvoicesController(IInvoiceRepository studentsRepository)
        {
            _studentRepository = studentsRepository;
        }

        [HttpGet("{customerId}")]
        public List<Invoice> Get_NoPagination(Guid customerId)
        {
            return _studentRepository.GetInvoices_NoPagination(customerId);
        }

        [MapToApiVersion("2.0")]
        [HttpGet("{customerId}")]
        public List<Invoice> Get_QuickAndDirtyPagination(Guid customerId, int page, int pageSize)
        {
            return _studentRepository.GetInvoices_QuickAndDirtyPagination(customerId, page, pageSize);
        }

        [MapToApiVersion("3.0")]
        [HttpGet("{customerId}")]
        public ActionResult<PageResponse<Invoice>> Get_RobustPagination(Guid customerId, int page, int pageSize)
        {
            const int maxPageSize = 10;

            ValidatePageRequest(page, pageSize, maxPageSize, out List<string> validationErrors);

            if (validationErrors.Count > 0)
                return BadRequest(string.Join(' ', validationErrors));

            var request = new GetInvoicesRequest
            {
                CustomerId = customerId,
                Page = page,
                PageSize = pageSize,
                Host = Request.Host.ToUriComponent(),
                Path = Request.Path.ToUriComponent(),
            };

            return _studentRepository.GetInvoices_RobustPagination(request);
        }

        private void ValidatePageRequest(int page, int pageSize, int maxPageSize, out List<string> validationErrors)
        {
            validationErrors = new List<string>();
            if (page <= 0)
                validationErrors.Add($"Parameter '{nameof(page)}' is required and must be greater than 0.");
            if (pageSize <= 0)
                validationErrors.Add($"Parameter '{nameof(pageSize)}' is required and must be greater than 0.");
            else if (pageSize > maxPageSize)
                validationErrors.Add($"Parameter '{nameof(pageSize)}' cannot be greater then {maxPageSize}.");
        }
    }
}
