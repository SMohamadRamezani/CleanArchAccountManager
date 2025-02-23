using MediatR;
using App.Role.Response;
using App.Wrapper;

namespace App.Role.Request
{
    public class DeleteRoleCommand : IRequest<ApiResponse<CommonAuthResponse>>
    {
        public string? RoleId { get; set; }
    }
}
