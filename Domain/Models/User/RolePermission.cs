﻿namespace Domain.Models.User
{
    public class RolePermission
    {
        public string RoleId { get; set; }
        public Role Role { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }

}
