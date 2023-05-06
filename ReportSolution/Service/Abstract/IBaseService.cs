using Model.Base;
using System.Linq.Expressions;

namespace Service.Abstract
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        int Create(TEntity entity);
        int Update(TEntity entity);
        int Delete(TEntity entity);
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        TEntity GetById(Guid id);
        IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null);
    }
}
