using MediatR;
using App.Authentication.Response;
using App.Wrapper;

namespace App.Authentication.Request
{
    public class LoginRequest : IRequest<ApiResponse<CommonRoleResponse>>
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
