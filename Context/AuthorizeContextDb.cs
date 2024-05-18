using Authorize.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Authenticate.Context
{

    public class AuthorizeContextDb : DbContext
    {
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public AuthorizeContextDb(DbContextOptions<AuthorizeContextDb> option) : base(option)
        {
            try
            {
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbCreator != null)
                {
                    if(!dbCreator.CanConnect()) dbCreator.Create();
                    if(!dbCreator.HasTables()) dbCreator.CreateTables();
                } 
            }   
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasOne(e => e.Role)
                .WithMany(e => e.Permissions)
                .HasForeignKey(e => e.RoleId)
                .IsRequired();

            modelBuilder.Entity<Permission>()
                .HasOne(e => e.Resource)
                .WithMany(e => e.Permissions)
                .HasForeignKey(e => e.ResourceId)
                .IsRequired();
        }
    }

}