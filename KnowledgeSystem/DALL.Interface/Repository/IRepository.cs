using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.Interface
{
    /// <summary>
    /// Interface for Repository pattern implementation
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// The method for entity creating
        /// </summary>
        /// <param name="entity"></param>
        void Create(TEntity entity);
        /// <summary>
        /// The method for entity updating
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);
        /// <summary>
        /// The method for getting entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(int id);
        /// <summary>
        /// The method for entity deleting
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
        /// <summary>
        ///  The method for getting all entities
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll();        
        /// <summary>
        /// The name for getting entity by predicate
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        TEntity GetByPredicate(Expression<Func<TEntity, bool>> f);
        
    }
}
