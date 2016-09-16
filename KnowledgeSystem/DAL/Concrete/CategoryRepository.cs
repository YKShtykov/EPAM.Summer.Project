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
        private readonly DbSet<Category> categories;
        private readonly DbSet<Skill> skills;

        /// <summary>
        /// Create CategoryRepository instance
        /// </summary>
        /// <param name="knowledgeContext">DbContext</param>
        public CategoryRepository(KnowledgeSystemContext knowledgeContext)
        {
            context = knowledgeContext;
            categories = context.Set<Category>();
            skills = context.Set<Skill>();
        }

        /// <summary>
        /// The method for creating new calegory entity in collection
        /// </summary>
        /// <param name="category"></param>
        public void Create(DalCategory category)
        {
            categories.Add(CategoryMapper.Map(category));
        }

        /// <summary>
        /// The method for updating exsisting category in collection
        /// </summary>
        /// <param name="dalCategory"></param>
        public void Update(DalCategory dalCategory)
        {
            var category = categories.FirstOrDefault(p => p.Id == dalCategory.Id);
            if (!ReferenceEquals(category,null))
            {
                category.Name = dalCategory.Name;
                category.Skills.Clear();
                foreach (var item in dalCategory.Skills)
                {
                    var skill = skills.FirstOrDefault(s => s.Id == item.Id);
                    category.Skills.Add(skill);
                }

                context.Entry(category).State = EntityState.Modified;
            }
        }

        /// <summary>
        /// The method for deleting category entity from collection
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var category = categories.Include(c => c.Skills).FirstOrDefault(c => c.Id == id);
            skills.RemoveRange(category.Skills);
            categories.Remove(category);
        }

        /// <summary>
        /// The method for getting category entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DalCategory</returns>
        public DalCategory Get(int id)
        {
            return CategoryMapper.Map(categories.FirstOrDefault(u => u.Id == id));
        }

        /// <summary>
        /// The method for getting all categories 
        /// </summary>
        /// <returns> DalCategory collection</returns>
        public IEnumerable<DalCategory> GetAll()
        {
            List<DalCategory> categoryList = new List<DalCategory>();
            foreach (Category item in categories.Include(c => c.Skills).Select(u => u))
            {
                categoryList.Add(CategoryMapper.Map(item));
            }

            return categoryList;
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

            return CategoryMapper.Map(categories.FirstOrDefault(func));
        }        
    }
}
