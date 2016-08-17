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
    public class SkillRepository : ISkillRepository
    {
        private readonly KnowledgeSystemContext context;

        public SkillRepository(KnowledgeSystemContext knowledgeContext)
        {
            context = knowledgeContext;
        }

        public void Create(DalSkill e)
        {
            var skill = SkillMapper.Map(e);
            context.Set<Skill>().Add(skill);

            context.Set<Category>().FirstOrDefault(c => c.Name == e.CategoryName).Skills.Add(skill);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var skill = context.Set<Skill>().FirstOrDefault(s => s.Id == id);
            context.Set<Skill>().Remove(skill);
            context.SaveChanges();
        }

        public IEnumerable<DalSkill> GetAll()
        {
            List<DalSkill> result = new List<DalSkill>();
            foreach (Skill item in context.Set<Skill>().Select(s => s).Include(s => s.Category))
            {
                result.Add(SkillMapper.Map(item));
            }

            return result;
        }

        public DalSkill GetById(int key)
        {
            return SkillMapper.Map(context.Set<Skill>().Select(s=>s).Include(s => s.Category).FirstOrDefault(u => u.Id == key));
        }

        public void Update(DalSkill entity)
        {
            var ormSkill = context.Set<Skill>().FirstOrDefault(s => s.Id == entity.Id);
            if (ormSkill != null)
            {
                ormSkill.Name = entity.Name;
                var category = context.Set<Category>().FirstOrDefault(c => c.Name == entity.CategoryName);
                ormSkill.Category = category;

                context.Entry(ormSkill).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public DalSkill GetByPredicate(Expression<Func<DalSkill, bool>> f)
        {
            throw new NotImplementedException();
        }
    }
}
