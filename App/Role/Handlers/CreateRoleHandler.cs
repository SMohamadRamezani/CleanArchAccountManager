using System.ComponentModel.DataAnnotations;
using MediatR;
using App.Interfaces;
using App.Role.Request;
using App.Role.Response;
using App.Utils;
using App.Wrapper;
using Domain.Interfaces;

namespace App.Role.Handlers
{
    public class CreateRoleHandler(IRoleRepository roleRepository, IPermissionRepository permissionRepository,IMessageService messageService) : IRequestHandler<CreateRoleCommand, ApiResponse<CommonAuthResponse>>
    {

        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IPermissionRepository _permissionRepository = permissionRepository;
        private readonly IMessageService _messageService = messageService;

        public async Task<ApiResponse<CommonAuthResponse>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {

            if (await _roleRepository.RoleExistsAsync(request.Name))
                throw new ValidationException(_messageService.GetMessage("RoleIsExists") );

            var role = new Domain.Models.User.Role { Name = request.Name };
            await _roleRepository.CreateRoleAsync(role);

            foreach (var permissionId in request.Permissions)
            {
                var permission = await _permissionRepository.GetPermissionByIdAsync(permissionId);
                if (permission != null)
                {
                    await _roleRepository.AssignPermissionToRoleAsync(role, permission);
                }
            }

            var commonAuthResponse=new CommonAuthResponse() { Result=role.Id};
            return new ApiResponse<CommonAuthResponse>(commonAuthResponse, "Successfuly");
        }
    }
}
