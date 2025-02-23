using MediatR;
using App.Role.Response;
using App.Wrapper;
using Domain.Models.User;

namespace App.Role.Request
{
    public class UpdateRoleCommand : IRequest<ApiResponse<CommonAuthResponse>>
    {
        public string? RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<RolePermission> Permissions { get; set; } = [];
    }
}
