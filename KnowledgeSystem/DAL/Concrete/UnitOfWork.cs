using DAL.Interface;
using System.Data.Entity;
using ORM;

namespace DAL
{
    /// <summary>
    /// Service class implements Unit of Work pattern
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// DbContext
        /// </summary>
        public DbContext Context { get; private set; }

        /// <summary>
        /// Categorys collection
        /// </summary>
        public ICategoryRepository Categories { get; set; }

        /// <summary>
        /// Users collection
        /// </summary>
        public IUserRepository Users { get; set; }

        /// <summary>
        /// Skills collection
        /// </summary>
        public ISkillRepository Skills { get; set; }

        /// <summary>
        /// Profiles collection
        /// </summary>
        public IProfileRepository Profiles { get; set; }

        /// <summary>
        /// Create UnitOfWork instance
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(DbContext context)
        {
            Context = context;
            Categories = new CategoryRepository((KnowledgeSystemContext)context);
            Users = new UserRepository((KnowledgeSystemContext)context);
            Skills = new SkillRepository((KnowledgeSystemContext)context);
            Profiles = new ProfileRepository((KnowledgeSystemContext)context);
        }

        /// <summary>
        /// Save changes in collections
        /// </summary>
        public void Commit()
        {
            if (Context != null)
            {
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Dispose context
        /// </summary>
        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }
    }
}
