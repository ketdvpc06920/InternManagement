﻿using InternManagement.DTOs.Requests;
using InternManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternManagement.Controlles
{
    [Authorize]
    [ApiController]
    [Route("api/roles")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetRoles([FromQuery] int page = 1, [FromQuery] int pageSize = 10,
           [FromQuery] string? search = "", [FromQuery] string sortColumn = "Id", [FromQuery] string sortOrder = "asc")
        {
            var response = _service.GetRoles(page, pageSize, search, sortColumn, sortOrder);
            return Ok(response);
        }


        [HttpGet("{id}")]
        public IActionResult GetRoleById(long id)
        {
            var response = _service.GetRoleById(id);
            return response.Code == 0 ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        public IActionResult CreateRole([FromBody] RoleRequest roleRequest)
        {
            var response = _service.CreateRole(roleRequest);
            return response.Code == 0 ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRole(long id, [FromBody] RoleRequest roleRequest)
        {
            var response = _service.UpdateRole(id, roleRequest);
            return response.Code == 0 ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRole(long id)
        {
            var response = _service.DeleteRole(id);
            return response.Code == 0 ? Ok(response) : BadRequest(response);
        }
    }
}
