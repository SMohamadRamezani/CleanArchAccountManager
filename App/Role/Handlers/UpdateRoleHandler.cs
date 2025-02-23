using System.ComponentModel.DataAnnotations;
using MediatR;
using App.Interfaces;
using App.Role.Request;
using App.Role.Response;
using App.Wrapper;
using Domain.Interfaces;

namespace App.Role.Handlers
{
    public class UpdateRoleHandler(IRoleRepository roleRepository, IMessageService messageService) : IRequestHandler<UpdateRoleCommand, ApiResponse<CommonAuthResponse>>
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IMessageService _messageService = messageService;
        public async Task<ApiResponse<CommonAuthResponse>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            if (!await _roleRepository.RoleExistsAsync(request.Name))
                throw new ValidationException(_messageService.GetMessage("RoleIsExists"));
            var role = _roleRepository.GetRoleByIdAsync(request.RoleId) ?? throw new ValidationException(_messageService.GetMessage("RoleNotFound"));
            await _roleRepository.UpdateRoleAsync(role.Result, request.Permissions);
            var commonAuthResponse = new CommonAuthResponse() { Result = "true" };
            return new ApiResponse<CommonAuthResponse>(commonAuthResponse, "Successfuly");
        }
    }
}
