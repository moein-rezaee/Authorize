using Authorize.Entities;

namespace Authorize.Interfaces
{
    public interface IUnitOfWorkRepository
    {
        public IRepository<Permission> Permissions { get; }
        public IRepository<Role> Roles { get; }
        public IRepository<Resource> Resources { get; }
    }
}
