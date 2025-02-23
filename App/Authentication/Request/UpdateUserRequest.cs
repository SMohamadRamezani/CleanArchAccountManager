using MediatR;
using App.Authentication.Response;
using App.Wrapper;

namespace App.Authentication.Request
{
    public class UpdateUserRequest : IRequest<ApiResponse<CommonRoleResponse>>
    {
        public string UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RoleId { get; set; }
    }
}
