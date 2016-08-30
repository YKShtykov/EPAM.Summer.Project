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
    /// <summary>
    /// Category repository class implements Repository pattern for category collection
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly KnowledgeSystemContext context;

        /// <summary>
        /// Create CategoryRepository instance
        /// </summary>
        /// <param name="knowledgeContext">DbContext</param>
        public CategoryRepository(KnowledgeSystemContext knowledgeContext)
        {
            context = knowledgeContext;
        }

        /// <summary>
        /// The method for creating new calegory entity in collection
        /// </summary>
        /// <param name="category"></param>
        public void Create(DalCategory category)
        {
            context.Set<Category>().Add(CategoryMapper.Map(category));
        }

        /// <summary>
        /// The method for updating exsisting category in collection
        /// </summary>
        /// <param name="category"></param>
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

        /// <summary>
        /// The method for deleting category entity from collection
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var category = context.Set<Category>().Include(c => c.Skills).FirstOrDefault(c => c.Id == id);
            context.Set<Skill>().RemoveRange(category.Skills);
            context.Set<Category>().Remove(category);
        }

        /// <summary>
        /// The method for getting category entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DalCategory</returns>
        public DalCategory Get(int id)
        {
            return CategoryMapper.Map(context.Set<Category>().FirstOrDefault(u => u.Id == id));
        }

        /// <summary>
        /// The method for getting all categories 
        /// </summary>
        /// <returns> DalCategory collection</returns>
        public IEnumerable<DalCategory> GetAll()
        {
            List<DalCategory> result = new List<DalCategory>();
            foreach (Category item in context.Set<Category>().Include(c => c.Skills).Select(u => u))
            {
                result.Add(CategoryMapper.Map(item));
            }

            return result;
        }

        /// <summary>
        /// The method for getting category entity by predicate
        /// </summary>
        /// <param name="f"></param>
        /// <returns>DalCategory</returns>
        public DalCategory GetByPredicate(Expression<Func<DalCategory, bool>> f)
        {
            var expr = ExpressionTransformer<DalCategory, Category>.Tranform(f);
            var func = expr.Compile();

            return CategoryMapper.Map(context.Set<Category>().FirstOrDefault(func));
        }        
    }
}
