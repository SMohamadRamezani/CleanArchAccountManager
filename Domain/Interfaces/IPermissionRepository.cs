using Domain.Models.User;

namespace Domain.Interfaces
{
    public interface IPermissionRepository
    {
        Task<Permission?> GetPermissionByIdAsync(int id);
    }
}
