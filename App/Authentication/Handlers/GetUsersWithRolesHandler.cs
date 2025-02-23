using MediatR;
using Microsoft.AspNetCore.Identity;
using App.Authentication.Queries;
using App.Authentication.Response;
using App.Wrapper;
using Domain.Models.User;

namespace App.Authentication.Handlers
{
    public class GetUsersWithRolesHandler(UserManager<User> userManager) : IRequestHandler<GetUsersWithRolesQuery, ApiResponse<List<UserWithRolesResponse>>>
    {

        private readonly UserManager<User> _userManager = userManager;
        public async Task<ApiResponse<List<UserWithRolesResponse>>> Handle(GetUsersWithRolesQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.ToList();

            var userList = new List<UserWithRolesResponse>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userList.Add(new UserWithRolesResponse
                {
                    Id = user.Id,
                    FullName = user.FirstName + " " + user.LastName,
                    Email = user.Email,
                    Roles = roles.ToList()
                });
            }

            return new ApiResponse<List<UserWithRolesResponse>>(userList, "Successfuly");
        }
    }
}
