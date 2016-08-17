using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using ORM.Mappers;

namespace ORM
{
    public partial class KnowledgeSystemContext :DbContext
    {
        public KnowledgeSystemContext()
              : base("name=KnowledgeSystemContext")
        {
            Database.SetInitializer(new DbInitializer());
        }

        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<UserSkill> UsersSkills { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }

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
