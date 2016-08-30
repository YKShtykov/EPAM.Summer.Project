using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public void Create(DalCategory category)
        {
            context.Set<Category>().Add(CategoryMapper.Map(category));
        }

        public void Update(DalCategory category)
        {
            var ormCategory = context.Set<Category>().FirstOrDefault(p => p.Id == category.Id);
            if (ormCategory != null)
            {
                ormCategory.Name = category.Name;
                ormCategory.Skills.Clear();
                foreach (var item in category.Skills)
                {
                    var skill = context.Set<Skill>().FirstOrDefault(s => s.Id == item.Id);
                    ormCategory.Skills.Add(skill);
                }

                context.Entry(ormCategory).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var category = context.Set<Category>().Include(c => c.Skills).FirstOrDefault(c => c.Id == id);
            context.Set<Skill>().RemoveRange(category.Skills);
            context.Set<Category>().Remove(category);
        }

        public DalCategory Get(int id)
        {
            return CategoryMapper.Map(context.Set<Category>().FirstOrDefault(u => u.Id == id));
        }

        public IEnumerable<DalCategory> GetAll()
        {
            List<DalCategory> result = new List<DalCategory>();
            foreach (Category item in context.Set<Category>().Include(c => c.Skills).Select(u => u))
            {
                result.Add(CategoryMapper.Map(item));
            }

            return result;
        }        

        public DalCategory GetByPredicate(Expression<Func<DalCategory, bool>> f)
        {
            var expr = ExpressionTransformer<DalCategory, Category>.Tranform(f);
            var func = expr.Compile();

            return CategoryMapper.Map(context.Set<Category>().FirstOrDefault(func));
        }        
    }
}
