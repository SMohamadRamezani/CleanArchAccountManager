using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using App.Authentication.Queries;
using App.Authentication.Request;
using Domain.Models.User;
using ApiEndPoint.Attributes;

namespace ApiEndPoint.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [Authorize]
        //[RequiredPermission("ViewUsers")]
        [HttpGet("GetUsersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var users = await _mediator.Send(new GetUsersWithRolesQuery());
            return users.Success ? Ok(users) : BadRequest(users);
        }

        [Authorize]
        [HttpPost("GetUserByUserName")]
        public async Task<IActionResult> GetUserByUserName(GetUserByUserNameQuery query)
        {

            var user = await _mediator.Send(query);
            return user.Success ? Ok(user) : BadRequest(user);
        }

        [Authorize]
        [RequiredPermission("ManageUsers")]
        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest command)
        {
            var userId = await _mediator.Send(command);
            return userId.Success ? Ok(userId) : BadRequest(userId); 
        }

        [Authorize]
        [RequiredPermission("ManageUsers")]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest command)
        {
            var userId = await _mediator.Send(command);
            return userId.Success ? Ok(userId) : BadRequest(userId);
        }

    }
}
