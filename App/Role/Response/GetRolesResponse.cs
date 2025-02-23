using Microsoft.AspNetCore.Identity;
using Domain.Models.User;

namespace App.Role.Response
{
    public class GetRolesResponse : IdentityRole
    {
        public List<User> Users { get; set; } = [];
        public ICollection<RolePermission> RolePermissions { get; set; } = [];

    }
}
