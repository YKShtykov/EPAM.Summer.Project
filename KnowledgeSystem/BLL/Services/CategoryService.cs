using System.Collections.Generic;
using BLL.Interface;
using DAL.Interface;
using BLL.Mappers;

namespace BLL
{
    /// <summary>
    /// Service for fork with BllCategories
    /// </summary>
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork uow;

        /// <summary>
        /// Create CategoryService instance
        /// </summary>
        /// <param name="uow"></param>
        public CategoryService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary>
        /// The method for creating new category
        /// </summary>
        /// <param name="category"></param>
        public void Create(BllCategory category)
        {
            uow.Categories.Create(CategoryMapper.Map(category));
            uow.Commit();
        }

        /// <summary>
        /// The method for updating existing category
        /// </summary>
        /// <param name="category"></param>
        public void Update(BllCategory category)
        {
            uow.Categories.Update(CategoryMapper.Map(category));
            uow.Commit();
        }

        /// <summary>
        /// The method for deleting category
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            uow.Categories.Delete(id);
            uow.Commit();
        }

        /// <summary>
        /// The method for getting category by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BllCategory</returns>
        public BllCategory Get(int id)
        {
            return CategoryMapper.Map(uow.Categories.Get(id));
        }

        /// <summary>
        /// The method for getting all categories
        /// </summary>
        /// <returns>BllCategories collection</returns>
        public IEnumerable<BllCategory> GetAll()
        {            
            return CategoryMapper.Map(uow.Categories.GetAll());
        }              
    }
}
