using Domain.Models.User;

namespace Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<bool> RoleExistsAsync(string roleName);
        Task CreateRoleAsync(Role role);
        Task AssignPermissionToRoleAsync(Role role, Permission permission);
        Task<Role> GetRoleByIdAsync(string roleId);
        Task DeleteRoleAsync(Role role);
        Task UpdateRoleAsync(Role role,List<RolePermission> rolePermission);
        Task<List<Role>> GetRolesAsync();
    }
}
