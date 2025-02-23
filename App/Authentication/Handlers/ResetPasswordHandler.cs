using System.ComponentModel.DataAnnotations;
using MediatR;
using App.Authentication.Request;
using App.Authentication.Response;
using App.Interfaces;
using App.Wrapper;
using Domain.Interfaces;

namespace App.Authentication.Handlers
{
    public class ResetPasswordHandler(IUserRepository userRepository, IMessageService messageService) : IRequestHandler<ResetPasswordRequest, ApiResponse<CommonRoleResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMessageService _messageService = messageService;

        public async Task<ApiResponse<CommonRoleResponse>> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
                throw new ValidationException(_messageService.GetMessage("UserNotFound"));
            user.Password = request.NewPassword; // Hash it in real scenarios

            await _userRepository.UpdateUserAsync(user);
            var commonAuthResponse=new CommonRoleResponse() {Result="true" };
            return new ApiResponse<CommonRoleResponse>(commonAuthResponse, _messageService.GetMessage("ResetedPassword"));
        }
    }
}
