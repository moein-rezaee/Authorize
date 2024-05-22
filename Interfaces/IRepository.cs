using System.Linq.Expressions;
using Authorize.Entities;

namespace Authorize.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class {
        List<TEntity> ToList();
        TEntity? Find(Guid id);
        TEntity? Find(Expression<Func<TEntity, bool>> condition);
        bool Any(Expression<Func<TEntity, bool>> condition);
        Guid Add(TEntity item);
        bool Edit(TEntity item);
        bool Delete(Guid id);
    }
}
