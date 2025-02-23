using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using App.Role.Queries;
using App.Role.Request;

namespace ApiEndPoint.Controllers.User
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")] // فقط ادمین می‌تواند نقش‌ها را مدیریت کند
    public class RoleController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _mediator.Send(new GetRolesQuery());            
            return roles.Success ? Ok(roles) : BadRequest(roles);
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command)
        {
            var result = await _mediator.Send(command);            
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("UpdateRole{roleId}")]
        public async Task<IActionResult> UpdateRole(string roleId, [FromBody] UpdateRoleCommand command)
        {
            command.RoleId = roleId;
            var result = await _mediator.Send(command);            
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeleteRole{roleId}")]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var result = await _mediator.Send(new DeleteRoleCommand { RoleId = roleId });

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }

}
