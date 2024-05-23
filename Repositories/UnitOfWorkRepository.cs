using Authorize.Context;
using Authorize.Entities;
using Authorize.Interfaces;

namespace Authorize.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private AuthorizeContextDb Db { get; init; }

        public IRepository<Permission> Permissions { get; private set; }
        public IRepository<Role> Roles { get; private set; }
        public IRepository<Resource> Resources { get; private set; }

        public UnitOfWorkRepository(AuthorizeContextDb db)
        {
            Db = db;
            Permissions = new Repository<Permission>(Db);
            Roles = new Repository<Role>(Db);
            Resources = new Repository<Resource>(Db);
        }

        public void Save()
        {
            Db.SaveChanges();
        }
    }
}