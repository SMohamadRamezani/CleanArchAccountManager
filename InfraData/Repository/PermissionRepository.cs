using Domain.Interfaces;
using Domain.Models.User;
using InfraData.Context;

namespace InfraData.Repository
{
    public class PermissionRepository(AppDbContext context) : IPermissionRepository
    {
        private readonly AppDbContext _context = context;

        public async Task<Permission?> GetPermissionByIdAsync(int id)
        {
            return await _context.Permissions.FindAsync(id);
        }
    }
}
