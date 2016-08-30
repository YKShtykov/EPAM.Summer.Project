using System.Collections.Generic;

namespace BLL.Interface
{
    /// <summary>
    /// Interface for work with categories
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// The method for creating new BllCategory
        /// </summary>
        /// <param name="category"></param>
        void Create(BllCategory category);
        /// <summary>
        /// The method for updating exsisting BllCategory
        /// </summary>
        /// <param name="category"></param>
        void Update(BllCategory category);
        /// <summary>
        /// The method for deleting BllCategory
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
        /// <summary>
        /// The method for getting BllCategory by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BllCatecory</returns>
        BllCategory Get(int id);
        /// <summary>
        /// The method for getting all BllCategories
        /// </summary>
        /// <returns>BllCategories collection</returns>
        IEnumerable<BllCategory> GetAll();       
    }
}
