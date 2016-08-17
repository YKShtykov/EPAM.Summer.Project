using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using DAL.Mappers;
using ORM;
using System.Data.Entity;

namespace DAL
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly KnowledgeSystemContext context;

        public CategoryRepository(KnowledgeSystemContext knowledgeContext)
        {
            context = knowledgeContext;
        }

        public void Create(DalCategory e)
        {
            context.Set<Category>().Add(CategoryMapper.Map(e));
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = context.Set<Category>().FirstOrDefault(c=>c.Id==id);
            context.Set<Category>().Remove(category);
            context.SaveChanges();
        }

        public IEnumerable<DalCategory> GetAll()
        {
            List<DalCategory> result = new List<DalCategory>();
            foreach (Category item in context.Set<Category>().Select(u => u))
            {
                result.Add(CategoryMapper.Map(item));
            }

            return result;
        }

        public DalCategory GetById(int key)
        {
            return CategoryMapper.Map(context.Set<Category>().FirstOrDefault(u => u.Id == key));
        }

        public DalCategory GetByPredicate(Expression<Func<DalCategory, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Update(DalCategory entity)
        {
            var ormCategory = context.Set<Category>().FirstOrDefault(p => p.Id == entity.Id);
            if (ormCategory != null)
            {
                ormCategory.Name = entity.Name;
                ormCategory.Skills.Clear();
                foreach (var item in entity.Skills)
                {
                    var skill = context.Set<Skill>().FirstOrDefault(s => s.Id == item.Id);
                    ormCategory.Skills.Add(skill);
                }

                context.Entry(ormCategory).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
