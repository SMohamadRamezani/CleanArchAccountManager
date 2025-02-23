using MediatR;
using App.Authentication.Response;
using App.Wrapper;

namespace App.Authentication.Request
{
    public class CreateUserRequest : IRequest<ApiResponse<CommonRoleResponse>> // برمی‌گرداند: شناسه کاربر جدید
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RoleId { get; set; }
    }
}
