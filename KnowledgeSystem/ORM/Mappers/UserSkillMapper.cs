using System.Data.Entity.ModelConfiguration;

namespace ORM.Mappers
{
    /// <summary>
    /// Service class for configurating UserSkill table
    /// </summary>
    class UserSkillMapper : EntityTypeConfiguration<UserSkill>
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
