using Microsoft.AspNetCore.Identity;

namespace Domain.Models.User
{
    public class Role : IdentityRole
    {     
        public List<User> Users { get; set; } = [];
        public ICollection<RolePermission> RolePermissions { get; set; } = [];
    }
}
