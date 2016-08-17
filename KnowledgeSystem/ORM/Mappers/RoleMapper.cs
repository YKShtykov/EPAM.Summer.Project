using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Mappers
{
    class RoleMapper: EntityTypeConfiguration<Role>
    {
        public RoleMapper()
        {
            ToTable("Role");

            HasKey(t => t.Id);

            Property(t => t.Name).IsRequired().HasMaxLength(32);
        }
    }
}
