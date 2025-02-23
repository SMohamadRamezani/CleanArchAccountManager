using System.ComponentModel.DataAnnotations;
using AutoMapper;
using MediatR;
using App.Authentication.Queries;
using App.Authentication.Response;
using App.Interfaces;
using App.Wrapper;
using Domain.Interfaces;

namespace App.Authentication.Handlers
{
    public class GetUserByUserNameHandler(IUserRepository userRepository, IMessageService messageService, IMapper mapper) : IRequestHandler<GetUserByUserNameQuery, ApiResponse<GetUserByUserNameResponse>>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMessageService _messageService = messageService;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse<GetUserByUserNameResponse>> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.userName) ?? throw new ValidationException(_messageService.GetMessage("UserNotFound"));
            var result = _mapper.Map<GetUserByUserNameResponse>(user);
            return new ApiResponse<GetUserByUserNameResponse>(result, "Successfuly");
        }
    }
}
