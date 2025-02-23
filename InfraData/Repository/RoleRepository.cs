using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models.User;
using InfraData.Context;

namespace InfraData.Repository
{
    public class RoleRepository(RoleManager<Role> roleManager, AppDbContext context) : IRoleRepository
    {
        private readonly RoleManager<Role> _roleManager = roleManager;
        private readonly AppDbContext _context = context;

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<Role> GetRoleByIdAsync(string roleId)
        {
            return await _context.Roles.Where(w => w.Id == roleId).FirstOrDefaultAsync();
        }
        public async Task CreateRoleAsync(Role role)
        {
            await _roleManager.CreateAsync(role);
        }

        public async Task AssignPermissionToRoleAsync(Role role, Permission permission)
        {
            _context.RolePermissions.Add(new RolePermission
            {
                RoleId = role.Id,
                PermissionId = permission.Id
            });

            await _context.SaveChangesAsync();
        }


        public async Task DeleteRoleAsync(Role role)
        {
            var roles = await _roleManager.FindByIdAsync(role.Id);

            if (role == null)
                throw new Exception("نقش یافت نشد");

            _context.RolePermissions.RemoveRange(_context.RolePermissions.Where(rp => rp.RoleId == role.Id));
            await _roleManager.DeleteAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(Role role, List<RolePermission> rolePermissions)
        {
            var roleFinded = await _roleManager.FindByIdAsync(role.Id);
            roleFinded.Name = role.Name;
            await _roleManager.UpdateAsync(role);
            var existingPermissions = _context.RolePermissions.Where(rp => rp.RoleId == role.Id);
            _context.RolePermissions.RemoveRange(existingPermissions);
            foreach (var permission in rolePermissions)
            {
                _context.RolePermissions.Add(new RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = permission.PermissionId
                });
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Role>> GetRolesAsync()
        {
            return _context.Roles.ToList();
        }
    }
}
