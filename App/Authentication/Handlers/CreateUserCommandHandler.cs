using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using MediatR;
using App.Authentication.Request;
using App.Authentication.Response;
using App.Interfaces;

using Domain.Interfaces;
using Domain.Models.User;
using App.Wrapper;

namespace App.Authentication.Handlers
{
    public class CreateUserCommandHandler(IUserRepository userRepository, IMessageService messageService) : IRequestHandler<CreateUserRequest, ApiResponse<CommonRoleResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMessageService _messageService = messageService;

        public async Task<ApiResponse<CommonRoleResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var userIsExists = _userRepository.GetUserByUsernameAsync(request.Username);
            // هش کردن رمز عبور
            var passwordHash = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(request.Password)));

            if (userIsExists != null) throw new ValidationException(_messageService.GetMessage("UserExists"));
            var user = new User
            {
                UserName = request.Username,
                Password = passwordHash,
                RoleId = request.RoleId
            };

            await _userRepository.AddUserAsync(user);
            var commonAuthResponse = new CommonRoleResponse() { Result = user.Id };

            return new ApiResponse<CommonRoleResponse>(commonAuthResponse, _messageService.GetMessage("UserCreated"));
        }
    }
}
