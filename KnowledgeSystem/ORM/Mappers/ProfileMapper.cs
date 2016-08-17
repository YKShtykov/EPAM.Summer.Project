using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;

namespace ORM.Mappers
{
    class ProfileMapper: EntityTypeConfiguration<Profile>
    {
        public ProfileMapper()
        {
            ToTable("Profile", "UserData");

            HasKey(t => t.Id);

            Property(t => t.FirstName).HasMaxLength(128);
            Property(t => t.MiddleName).HasMaxLength(128);
            Property(t => t.LastName).HasMaxLength(128);
            Property(t => t.City).HasMaxLength(128);
            Property(t => t.AdditionalInfo).HasMaxLength(1000);
            Property(t => t.BirthDate);
            Property(t => t.Gender).HasMaxLength(16);
            Property(t => t.RelationshipStatus).HasMaxLength(16);
            Property(t => t.ImageLink).HasMaxLength(200);

            HasRequired(t => t.User).WithOptional(u => u.Profile);
        }
    }
}
