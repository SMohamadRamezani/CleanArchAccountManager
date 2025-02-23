using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using MediatR;
using App.Authentication.Request;
using App.Authentication.Response;
using App.Interfaces;
using App.Wrapper;
using Domain.Interfaces;

namespace App.Authentication.Handlers
{
    public class UpdateUserCommandHandler(IUserRepository userRepository, IMessageService messageService) : IRequestHandler<UpdateUserRequest, ApiResponse<CommonRoleResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMessageService _messageService = messageService;
        public async Task<ApiResponse<CommonRoleResponse>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(request.UserId) ?? throw new ValidationException(_messageService.GetMessage("UserNotFound"));
            user.UserName = request.Username;

            if (!string.IsNullOrEmpty(request.Password))
            {
                user.Password = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(request.Password)));
            }

            user.RoleId = request.RoleId;

            await _userRepository.UpdateUserAsync(user);
            var commonAuthResponse= new CommonRoleResponse() { Result="true"};
            return new ApiResponse<CommonRoleResponse>(commonAuthResponse, _messageService.GetMessage("UserUpdated"));
        }
    }
}
