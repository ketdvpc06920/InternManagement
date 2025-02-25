using InternManagement.DTOs.Requests;
using InternManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternManagement.Controllers
{
    [Authorize]
    [ApiController]
        [Route("api/allow-access")]
        public class AllowAccessController : ControllerBase
        {
            private readonly IAllowAccessService _service;

            public AllowAccessController(IAllowAccessService service)
            {
                _service = service;
            }

            [HttpGet]
            public IActionResult GetAllowAccess([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
               [FromQuery] string? search = "", [FromQuery] string sortColumn = "Id", [FromQuery] string sortOrder = "asc")
            {
                var response = _service.GetAllowAccess(page, pageSize, search, sortColumn, sortOrder);
                return Ok(response);
            }


            [HttpGet("{id}")]
            public IActionResult GetAllowAccessById(long id)
            {
                var response = _service.GetAllowAccessById(id);
                return response.Code == 0 ? Ok(response) : NotFound(response);
            }

            [HttpPost]
            public IActionResult CreateAllowAccess([FromBody] AllowAccessRequest allowAccessRequest)
            {
                var response = _service.CreateAllowAccess(allowAccessRequest);
                return response.Code == 0 ? Ok(response) : BadRequest(response);
            }

            [HttpPut("{id}")]
            public IActionResult UpdateAllowAccess(long id, [FromBody] AllowAccessRequest allowAccessRequest)
            {
                var response = _service.UpdateAllowAccess(id, allowAccessRequest);
                return response.Code == 0 ? Ok(response) : BadRequest(response);
            }

            [HttpDelete("{id}")]
            public IActionResult DeleteAllowAccess(long id)
            {
                var response = _service.DeleteAllowAccess(id);
                return response.Code == 0 ? Ok(response) : BadRequest(response);
            }
        }
}
