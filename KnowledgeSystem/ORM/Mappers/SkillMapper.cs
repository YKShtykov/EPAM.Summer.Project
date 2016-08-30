using System.Data.Entity.ModelConfiguration;

namespace ORM.Mappers
{
    /// <summary>
    /// Service class for configurating Skill table
    /// </summary>
    class SkillMapper : EntityTypeConfiguration<Skill>
    {
        public SkillMapper()
        {
            ToTable("Skill");

            HasKey(s => s.Id);
            Property(s => s.Name).IsRequired().HasMaxLength(20);
        }
    }
}
