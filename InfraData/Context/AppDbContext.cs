using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Domain.Models.User;

namespace InfraData.Context
{
    
    public class AppDbContext : IdentityDbContext<User, Role, string,
    IdentityUserClaim<string>, IdentityUserRole<string>, IdentityUserLogin<string>,
    IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; } // اضافه کردن DbSet برای نقش‌ها
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // رابطه RolePermission
            modelBuilder.Entity<RolePermission>()
        .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            //// تنظیم داده‌های پیش‌فرض
            //modelBuilder.Entity<Role>().HasData(
            //    new Role { Name = "Admin" },
            //    new Role { Name = "User" }
            //);
            //modelBuilder.Entity<Permission>().HasData(
            //    new Permission { Id = 1, Name = "ViewUsers" },
            //    new Permission { Id = 2, Name = "ManageUsers" }
            //);
            //modelBuilder.Entity<RolePermission>().HasData(
            //    new RolePermission { RoleId = 1, PermissionId = 1 }, // Admin: ViewUsers
            //    new RolePermission { RoleId = 1, PermissionId = 2 }, // Admin: ManageUsers
            //    new RolePermission { RoleId = 2, PermissionId = 1 }  // User: ViewUsers
            //);
        }
    }
}
