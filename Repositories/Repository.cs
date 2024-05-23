using System.Linq.Expressions;
using Authorize.Context;
using Authorize.Interfaces;
using Extentions;
using Mapster;

namespace Authorize.Repositories
{
    public class Repository<TEntity>(AuthorizeContextDb db): IRepository<TEntity> where TEntity : class
    {
        private readonly AuthorizeContextDb _db = db;

        public List<TEntity> ToList() => [.. _db.Set<TEntity>()];

        public TEntity? Find(Guid id) => _db.Set<TEntity>().Find(id);
        public TEntity? Find(Expression<Func<TEntity, bool>> expression) => _db.Set<TEntity>().Find(expression);

        public void Add(TEntity item)
        {
            _db.Set<TEntity>().Add(item);
        }

        public bool Edit<TDto>(TDto item) where TDto: class
        {
            Guid id = item.GetIdAsGuid();
            TEntity? entity = Find(id);
            if (entity is null)
            {   
                return false;
            }
            entity = item.Adapt<TEntity>();
            _db.Set<TEntity>().Update(entity);
            return true;
        }

        public bool Delete(Guid id)
        {
            TEntity? entity = Find(id);
            if (entity is null)
            {   
                return false;
            }
            _db.Set<TEntity>().Remove(entity);
            return true;
        }

        public bool Any(Expression<Func<TEntity, bool>> condition)
        {
            TEntity? entity = Find(condition);
            return entity is not null;
        }
    }
}
