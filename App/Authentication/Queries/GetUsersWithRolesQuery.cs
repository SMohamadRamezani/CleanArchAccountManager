using MediatR;
using App.Authentication.Response;
using App.Wrapper;
using Domain.Models.User;

namespace App.Authentication.Queries
{
    public class GetUsersWithRolesQuery : IRequest<ApiResponse<List<UserWithRolesResponse>>> { }

}
