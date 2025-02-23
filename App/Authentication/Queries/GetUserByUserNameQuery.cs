using MediatR;
using App.Authentication.Response;
using App.Wrapper;

namespace App.Authentication.Queries
{
    public class GetUserByUserNameQuery : IRequest<ApiResponse<GetUserByUserNameResponse>>
    {
        public string? userName { get; set; }
    }
}
