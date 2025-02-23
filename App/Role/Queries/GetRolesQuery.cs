using MediatR;
using App.Role.Response;
using App.Wrapper;

namespace App.Role.Queries
{
    public class GetRolesQuery : IRequest<ApiResponse<List<GetRolesResponse>>> { }    
}
