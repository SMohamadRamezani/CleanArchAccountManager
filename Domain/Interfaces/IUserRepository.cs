using Domain.Models.User;

namespace Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<string> AddUserAsync(User user);
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(string id);
        Task<User?> GetUserByUsernameAsync(string Username);
        Task<User?> GetUserByEmailAsync(string email);
        Task UpdateUserAsync(User user);
        Task<List<User>> GetUsersWithRolesAsync();
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
        Task<bool> HasPermissionAsync(string Id, string permission);
        Task<bool> DeleteUserAsync(string userName);
        Task<bool> ResetPasswordAsync(string userName, string token, string password);
        Task<bool> ChangePasswordAsync(string userName, string oldPassword, string newPassword);
        bool ActiveUser(string id);
        bool DeActiveUser(string id);
    }
}
