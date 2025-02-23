using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models.User;
using InfraData.Context;

namespace InfraData.Repository
{
    public class UserRepository(AppDbContext appDbContext, UserManager<User> userManager) : IUserRepository
    {        
        private readonly AppDbContext _appDbContext = appDbContext;
        private readonly UserManager<User> _userManager= userManager;

        public bool ActiveUser(string id)
        {                       
            
            var finduser = _appDbContext.Users.Where(w => w.Id== id).FirstOrDefault();
            if (finduser != null)
            {
                finduser.IsActive = true;
                _appDbContext.Update(finduser);
                _appDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeActiveUser(string id)
        {            
            var finduser = _appDbContext.Users.Where(w => w.Id == id).FirstOrDefault();
            if (finduser != null)
            {
                finduser.IsActive = false;
                _appDbContext.Update(finduser);
                _appDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<string> AddUserAsync(User user) {
            await _userManager.CreateAsync(user);
            return user.Id;
        } 

        public async Task UpdateUserAsync(User user)
        {
            await _userManager.UpdateAsync(user);
        }

        public async Task<List<User>> GetUsersWithRolesAsync()
        {
            return await _appDbContext.Users.Include(u => u.Role).ToListAsync();
        }

        public async Task<bool> ChangePasswordAsync(string userName, string oldPassword, string newPassword)
        {
            var user= await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
                return true;
            }
            return false;
        }        

        public async Task<bool> DeleteUserAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
                return true;
            }
            return false;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user= await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return user;
            }
            return null;
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken) => await _appDbContext.Users.Include(u => u.RefreshToken).FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _appDbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<bool> ResetPasswordAsync(string userName, string token, string password)
        {
            var user= _userManager.FindByNameAsync(userName);
            if (user.Result != null)
            {
                await _userManager.ResetPasswordAsync(user.Result, token, password);
                return true;
            }
            return false;
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {               
            return await _appDbContext.Users.Include(u => u.Role)
            .ThenInclude(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<bool> HasPermissionAsync(string Id, string permission)
        {
            var user = await GetUserByIdAsync(Id);
            if (user == null) return false;

            return user.Role.RolePermissions.Any(rp => rp.Permission.Name== permission);
        }        
    }
}
