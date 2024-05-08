using Authorize.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authenticate.Context
{

    public class AuthorizeContextDb(DbContextOptions<AuthorizeContextDb> option) : DbContext(option)
    {
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleResource> RolesResources { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<User>()
            //     .HasOne(e => e.Organization)
            //     .WithMany(e => e.Users)
            //     .HasForeignKey(e => e.OrganizationId)
            //     .IsRequired();
        }
    }

}