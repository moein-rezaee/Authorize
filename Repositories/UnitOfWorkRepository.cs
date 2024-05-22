using Authorize.Context;
using Authorize.Entities;
using Authorize.Interfaces;

namespace Authorize.Repositories
{
    public class UnitOfWorkRepository(AuthorizeContextDb db) : IUnitOfWorkRepository
    {
        private AuthorizeContextDb Db { get; init; } = db;
        public IRepository<Permission> Permissions
        {
            get
            {
                if (Permissions is null)
                    return new Repository<Permission>(Db);
                return Permissions;
            }
        }
        public IRepository<Role> Roles
        {
            get
            {
                if (Roles is null)
                    return new Repository<Role>(Db);
                return Roles;
            }
        }
        public IRepository<Resource> Resources
        {
            get
            {
                if (Resources is null)
                    return new Repository<Resource>(Db);
                return Resources;
            }
        }
    }
}