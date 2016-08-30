using System.Collections.Generic;
using BLL.Interface;
using DAL.Interface;
using BLL.Mappers;

namespace BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork uow;

        public CategoryService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void Create(BllCategory category)
        {
            uow.Categories.Create(CategoryMapper.Map(category));
            uow.Commit();
        }

        public void Update(BllCategory category)
        {
            uow.Categories.Update(CategoryMapper.Map(category));
            uow.Commit();
        }

        public void Delete(int id)
        {
            uow.Categories.Delete(id);
            uow.Commit();
        }

        public BllCategory Get(int id)
        {
            return CategoryMapper.Map(uow.Categories.Get(id));
        }

        public IEnumerable<BllCategory> GetAll()
        {            
            return CategoryMapper.Map(uow.Categories.GetAll());
        }              
    }
}
