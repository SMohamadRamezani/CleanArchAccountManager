using MediatR;
using App.Role.Response;
using App.Wrapper;

namespace App.Role.Request
{
    public class CreateRoleCommand : IRequest<ApiResponse<CommonAuthResponse>>
    {
        public string Name { get; set; } = string.Empty;
        public List<int> Permissions { get; set; } = [];
    }
}
