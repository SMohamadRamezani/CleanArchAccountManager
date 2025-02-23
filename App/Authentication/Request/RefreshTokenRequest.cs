using MediatR;
using App.Authentication.Response;
using App.Wrapper;

namespace App.Authentication.Request
{
    public class RefreshTokenRequest : IRequest<ApiResponse<CommonRoleResponse>>
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
