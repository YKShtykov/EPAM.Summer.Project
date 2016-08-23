using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        TEntity Get(int id);
        void Delete(int id);          
        IEnumerable<TEntity> GetAll();        
        TEntity GetByPredicate(Expression<Func<TEntity, bool>> f);
        
    }
}
