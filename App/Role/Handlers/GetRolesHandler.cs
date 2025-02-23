using AutoMapper;
using MediatR;
using App.Role.Queries;
using App.Role.Response;
using App.Wrapper;
using Domain.Interfaces;

namespace App.Role.Handlers
{
    public class GetRolesHandler(IRoleRepository roleRepository, IMapper mapper) : IRequestHandler<GetRolesQuery, ApiResponse<List<GetRolesResponse>>>
    {
        private readonly IRoleRepository _roleRepository = roleRepository;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse<List<GetRolesResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            var role =await _roleRepository.GetRolesAsync();
            var result= _mapper.Map<List<GetRolesResponse>>(role);
            return new ApiResponse<List<GetRolesResponse>>(result, "Successfuly");
        }
    }
}
