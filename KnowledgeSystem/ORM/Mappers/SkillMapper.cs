using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Mappers
{
    class SkillMapper: EntityTypeConfiguration<Skill>
    {
        public SkillMapper()
        {
            ToTable("Skill");

            HasKey(s => s.Id);
            Property(s => s.Name).IsRequired().HasMaxLength(20);
        }
    }
}
