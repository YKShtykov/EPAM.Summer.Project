using System.Collections.Generic;
using System.Linq;
using MvcApp.Infrastructure;

namespace MvcApp.ViewModels
{
    /// <summary>
    /// Pagination model for storing entities 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericPaginationModel<T>
    {
        /// <summary>
        /// Create generic pagination model
        /// </summary>
        public GenericPaginationModel()
        {
        }
        /// <summary>
        /// Create generic pagination model
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="entities"></param>
        public GenericPaginationModel(int page, int pageSize, IEnumerable<T> entities)
        {
            Entities = entities.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            Pagination = new Pagination { PageNumber = page, PageSize = pageSize, TotalItems = entities.ToList().Count };
        }

        /// <summary>
        /// Pagination info
        /// </summary>
        public Pagination Pagination { get; set; }
        /// <summary>
        /// Storing entities
        /// </summary>
        public IList<T> Entities { get; set; }
    }
}