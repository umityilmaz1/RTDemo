using Model.Base;
using System.Linq.Expressions;

namespace Core.DataAccess
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        int Create(TEntity entity);
        int CreateRange(IList<TEntity> entities);
        int Update(TEntity entity);
        int Delete(TEntity entity);
        void RemoveRange(IList<TEntity> entities);
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        TEntity GetById(int id);
        IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null);
    }
}
