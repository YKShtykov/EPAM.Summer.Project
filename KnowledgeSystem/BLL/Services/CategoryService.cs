using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL.Interface;
using BLL.Mappers;

namespace BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork uow;
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(IUnitOfWork uow, ICategoryRepository repository)
        {
            this.uow = uow;
            this.categoryRepository = repository;
        }

        public void Create(BllCategory category)
        {
            categoryRepository.Create(CategoryMapper.Map(category));
        }

        public void Delete(int id)
        {
            categoryRepository.Delete(id);
        }

        public IEnumerable<BllCategory> GetAll()
        {
            List<BllCategory> result = new List<BllCategory>();
            foreach (var item in categoryRepository.GetAll())
            {
                result.Add(CategoryMapper.Map(item));
            }

            return result;
        }

        public BllCategory GetById(int id)
        {
            return CategoryMapper.Map(categoryRepository.Get(id));
        }

        public void Update(BllCategory entity)
        {
            categoryRepository.Update(CategoryMapper.Map(entity));
        }
    }
}
