
using System.ComponentModel.DataAnnotations;
using MediatR;
using App.Authentication.Request;
using App.Authentication.Response;
using App.Interfaces;
using App.Wrapper;
using Domain.Interfaces;

namespace App.Authentication.Handlers
{
    public class ChangePasswordHandler(IUserRepository userRepository, IMessageService messageService) : IRequestHandler<ChangePasswordRequest, ApiResponse<CommonRoleResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMessageService _messageService = messageService;

        public async Task<ApiResponse<CommonRoleResponse>> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetUserByUsernameAsync(request.UserName) ?? throw new ValidationException(_messageService.GetMessage("UserNotFound"));
            if (user.Password != request.OldPassword) throw new ValidationException(_messageService.GetMessage("OldPasswordIsIncorrect"));

            user.Password = request.NewPassword; // Hash it in real scenarios
            await _userRepository.UpdateUserAsync(user);
            var changePasswordresponse = new CommonRoleResponse { Result = "true" };
            return new ApiResponse<CommonRoleResponse>(changePasswordresponse, _messageService.GetMessage("UserUpdated"));
        }
    }
}
