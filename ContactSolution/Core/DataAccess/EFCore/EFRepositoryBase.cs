using Microsoft.EntityFrameworkCore;
using Model.Base;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class EFRepositoryBase<TEntity, TContext> : IRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : DbContext, new()
    {
        TContext _context = EFContext<TContext>.GetInstance();

        public int Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return _context.SaveChanges();
        }

        public int CreateRange(IList<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            return _context.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return _context.SaveChanges();
        }

        public int Delete(TEntity entity)
        {
            entity.IsActive = false;
            return Update(entity);
        }

        public void RemoveRange(IList<TEntity> entities)
        {
            _context.RemoveRange(entities);
            _context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().SingleOrDefault(filter);
        }

        public IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? _context.Set<TEntity>() : _context.Set<TEntity>().Where(filter);
        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().SingleOrDefault(a => a.ID == id);
        }
    }
}
