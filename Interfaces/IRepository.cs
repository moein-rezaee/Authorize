using System.Linq.Expressions;
using Authorize.Entities;

namespace Authorize.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class {
        List<TEntity> ToList();
        TEntity? Find(Guid id);
        TEntity? Find(Expression<Func<TEntity, bool>> condition);
        bool Any(Expression<Func<TEntity, bool>> condition);
        void Add(TEntity item);
        bool Edit<TDto>(TDto item) where TDto: class;
        bool Delete(Guid id);
    }
}
