using System.Data.Entity;
using ORM.Mappers;
using CryptoService.Interface;

namespace ORM
{
    /// <summary>
    /// Service ORM Layout class, DbContext inheritor
    /// </summary>
    public partial class KnowledgeSystemContext :DbContext
    {
        /// <summary>
        /// Creates context and starts DbInitialiser
        /// </summary>
        public KnowledgeSystemContext(IPasswordService passwordService)
              : base("name=KnowledgeSystemContext")
        {
            Database.SetInitializer(new DbInitializer(passwordService));
        }

        /// <summary>
        /// Roles collection
        /// </summary>
        public virtual DbSet<Role> Roles { get; set; }
        /// <summary>
        /// Users collection
        /// </summary>
        public virtual DbSet<User> Users { get; set; }
        /// <summary>
        /// Skills collection
        /// </summary>
        public virtual DbSet<Skill> Skills { get; set; }
        /// <summary>
        /// UsersSkills collection
        /// </summary>
        public virtual DbSet<UserSkill> UsersSkills { get; set; }
        /// <summary>
        /// Categories Collection
        /// </summary>
        public virtual DbSet<Category> Categories { get; set; }
        /// <summary>
        /// Profiles Collection
        /// </summary>
        public virtual DbSet<Profile> Profiles { get; set; }


        /// <summary>
        /// Service ORM Layout class, needs to matching objects and tables
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new RoleMapper());
            modelBuilder.Configurations.Add(new UserMapper());
            modelBuilder.Configurations.Add(new SkillMapper());
            modelBuilder.Configurations.Add(new CategoryMapper());
            modelBuilder.Configurations.Add(new ProfileMapper());
            modelBuilder.Configurations.Add(new UserSkillMapper());
        }
    }
}
