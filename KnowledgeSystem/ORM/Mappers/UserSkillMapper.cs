using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Mappers
{
    class UserSkillMapper: EntityTypeConfiguration<UserSkill>
    {
        public UserSkillMapper()
        {
            ToTable("UserSkill");

            Property(us => us.Level);

            HasRequired(us => us.User)
                .WithMany(u => u.UsersSkills);
            HasRequired(us => us.Skill)
                .WithMany(s => s.UsersSkills);
        }
    }
}
