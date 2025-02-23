using System.ComponentModel.DataAnnotations;
using MediatR;
using App.Interfaces;
using App.Role.Request;
using App.Role.Response;
using App.Wrapper;
using Domain.Interfaces;

namespace App.Role.Handlers
{
    public class DeleteRoleHandler(IRoleRepository roleRepository, IMessageService messageService) : IRequestHandler<DeleteRoleCommand, ApiResponse<CommonAuthResponse>>
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IMessageService _messageService = messageService;

        public async Task<ApiResponse<CommonAuthResponse>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = _roleRepository.GetRoleByIdAsync(request.RoleId) ?? throw new ValidationException(_messageService.GetMessage("RoleNotFound"));
            await _roleRepository.DeleteRoleAsync(role.Result);
            var commonAuthResponse = new CommonAuthResponse() { Result = "true" };
            return new ApiResponse<CommonAuthResponse>(commonAuthResponse, "Successfuly");
        }
    }
}
