using System.ComponentModel.DataAnnotations;
using MediatR;
using App.Authentication.Request;
using App.Authentication.Response;
using App.Interfaces;
using App.Wrapper;
using Domain.Interfaces;

namespace App.Authentication.Handlers
{
    public class LoginCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IMessageService messageService) : IRequestHandler<LoginRequest, ApiResponse<CommonRoleResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly IMessageService _messageService = messageService;

        public async Task<ApiResponse<CommonRoleResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (user == null || user?.Password != request.Password) // Hash comparison for simplicity
                throw new ValidationException( _messageService.GetMessage("UnAuthorizedUser"));
                        
            var token = _jwtTokenGenerator.GenerateToken(user);
            user.RefreshToken = _jwtTokenGenerator.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateUserAsync(user);
            var commonAuthResponse=new CommonRoleResponse{ Result=token};
            return new ApiResponse<CommonRoleResponse>(commonAuthResponse, "Successully");
        }
    }
}
