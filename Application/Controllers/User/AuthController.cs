using MediatR;
using Microsoft.AspNetCore.Mvc;
using App.Authentication.Request;

namespace ApiEndPoint.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {

        private readonly IMediator _mediator = mediator;
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest command)
        {
            var result = await _mediator.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
          
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest command)
        {
            var result = await _mediator.Send(command);
            return result.Success ? Ok(new { Token = result.Data }) : BadRequest(result);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        //[Authorize(Roles = "Admin")] // فقط مدیران مجاز هستند
        //[HttpGet("users")]
        //public async Task<IActionResult> GetUsers([FromServices] IUserRepository userRepository)
        //{
        //    var users = await userRepository.GetUsersWithRolesAsync();
        //    return Ok(users);
        //}

        //[Authorize(Roles = "Admin")] // فقط مدیران مجاز هستند
        //[HttpPut("UpdateUser{id}")]
        //public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserCommand command)
        //{
        //    command.UserId = id;
        //    await _mediator.Send(command);
        //    return NoContent();
        //}        
    }
}
