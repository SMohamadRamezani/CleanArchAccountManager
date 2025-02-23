using MediatR;
using App.Authentication.Response;
using App.Wrapper;

namespace App.Authentication.Request
{
    public class ChangePasswordRequest : IRequest<ApiResponse<CommonRoleResponse>>
    {
        public string? UserName { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
    }
}
