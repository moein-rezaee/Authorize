using Authorize.Context;
using Authorize.Entities;
using Authorize.Interfaces;

namespace Authorize.Repositories
{
    public class UnitOfWorkRepository : IUnitOfWorkRepository
    {
        private AuthorizeContextDb Db { get; init; }
        public IRepository<Permission> Permissions => new Repository<Permission>(Db);
        public IRepository<Role> Roles => new Repository<Role>(Db);
        public IRepository<Resource> Resources => new Repository<Resource>(Db);

        public UnitOfWorkRepository(AuthorizeContextDb db)
        {
            Db = db;
        }
    }
}