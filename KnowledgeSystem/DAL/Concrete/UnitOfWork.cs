using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using System.Data.Entity;
using ORM;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; private set; }
        public ICategoryRepository Categories { get; set; }
        public IUserRepository Users { get; set; }
        public ISkillRepository Skills { get; set; }
        public IProfileRepository Profiles { get; set; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
            Categories = new CategoryRepository((KnowledgeSystemContext)context);
            Users = new UserRepository((KnowledgeSystemContext)context);
            Skills = new SkillRepository((KnowledgeSystemContext)context);
            Profiles = new ProfileRepository((KnowledgeSystemContext)context);
        }

        public void Commit()
        {
            if (Context != null)
            {
                Context.SaveChanges();
            }
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }
    }
}
