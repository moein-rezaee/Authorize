using System.Linq.Expressions;
using Authorize.Context;
using Authorize.Entities;
using Authorize.Interfaces;
using Extentions;

namespace Authorize.Repositories
{
    public class Repository<TEntity>(AuthorizeContextDb db): IRepository<TEntity> where TEntity : class
    {
        private readonly AuthorizeContextDb _db = db;

        public List<TEntity> ToList() => [.. _db.Set<TEntity>()];

        public TEntity? Find(Guid id) => _db.Set<TEntity>().Find(id);
        public TEntity? Find(Expression<Func<TEntity, bool>> expression) => _db.Set<TEntity>().Find(expression);

        public Guid Add(TEntity item)
        {
            _db.Set<TEntity>().Add(item);
            _db.SaveChanges();
            return item.GetIdAsGuid();
        }

        public bool Edit(TEntity item)
        {
            Guid id = item.GetIdAsGuid();
            TEntity? entity = Find(id);
            if (entity is null)
            {   
                return false;
            }
            entity = item;
            _db.Set<TEntity>().Update(entity);
            _db.SaveChanges();
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
            _db.SaveChanges();
            return true;
        }

        public bool Any(Expression<Func<TEntity, bool>> condition)
        {
            TEntity? entity = Find(condition);
            return entity is not null;
        }
    }
}
