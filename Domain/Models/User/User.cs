using Microsoft.AspNetCore.Identity;

namespace Domain.Models.User
{
    public class User : IdentityUser
    {        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string NationalCode { get; set; } = string.Empty;        
        public string Password { get; set; } = string.Empty;                 
        public bool IsActive { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        //public List<RolePermission> RolePermissions { get; set; } = [];        

        public string RoleId { get; set; }
        public Role Role { get; set; }

    }
}
