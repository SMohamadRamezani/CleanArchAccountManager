using System.ComponentModel.DataAnnotations;
using MediatR;
using App.Authentication.Request;
using App.Authentication.Response;
using App.Interfaces;
using App.Wrapper;
using Domain.Interfaces;

namespace App.Authentication.Handlers
{
    public class RefreshTokenHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IMessageService messageService) : IRequestHandler<RefreshTokenRequest, ApiResponse<CommonRoleResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator = jwtTokenGenerator;
        private readonly IMessageService _messageService = messageService;

        public async Task<ApiResponse<CommonRoleResponse>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(request.RefreshToken);
            if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                throw new ValidationException(_messageService.GetMessage("TokenInvalid"));
            var newJwtToken = _jwtTokenGenerator.GenerateToken(user);
            user.RefreshToken = _jwtTokenGenerator.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepository.UpdateUserAsync(user);
            var commonAuthResponse=new CommonRoleResponse() {  Result=newJwtToken};
            return new ApiResponse<CommonRoleResponse>(commonAuthResponse, "Successfuly");
        }
    }
}
