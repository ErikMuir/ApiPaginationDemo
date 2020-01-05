using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ApiPaginationDemo.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentsRepository)
        {
            _studentRepository = studentsRepository;
        }

        [HttpGet("{instructorId}")]
        public List<Student> Get(Guid instructorId)
        {
            return _studentRepository.GetStudents(instructorId);
        }

        [MapToApiVersion("2.0")]
        [HttpGet("{instructorId}")]
        public ActionResult<PageResponse<Student>> Get(Guid instructorId, int page, int pageSize)
        {
            const int maxPageSize = 10;

            ValidatePageRequest(page, pageSize, maxPageSize, out List<string> errors);

            if (errors.Count > 0)
                return BadRequest(string.Join(' ', errors));

            var request = new GetStudentsRequest
            {
                InstructorId = instructorId,
                Page = page,
                PageSize = pageSize,
                Host = Request.Host.ToUriComponent(),
                Path = Request.Path.ToUriComponent(),
            };

            return _studentRepository.GetStudents(request);
        }

        private void ValidatePageRequest(int page, int pageSize, int maxPageSize, out List<string> errors)
        {
            errors = new List<string>();
            if (page <= 0)
                errors.Add($"Parameter '{nameof(page)}' is required and must be greater than 0.");
            if (pageSize <= 0)
                errors.Add($"Parameter '{nameof(pageSize)}' is required and must be greater than 0.");
            else if (pageSize > maxPageSize)
                errors.Add($"Parameter '{nameof(pageSize)}' cannot be greater then {maxPageSize}.");
        }
    }
}
