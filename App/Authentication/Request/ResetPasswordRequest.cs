using MediatR;
using App.Authentication.Response;
using App.Wrapper;

namespace App.Authentication.Request
{
    public class ResetPasswordRequest : IRequest<ApiResponse<CommonRoleResponse>>
    {
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? NewPassword { get; set; }
    }
}
