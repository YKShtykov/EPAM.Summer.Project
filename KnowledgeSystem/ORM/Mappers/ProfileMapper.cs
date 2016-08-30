using System.Data.Entity.ModelConfiguration;

namespace ORM.Mappers
{
    /// <summary>
    /// Service class for configurating Profile table
    /// </summary>
    class ProfileMapper : EntityTypeConfiguration<Profile>
    {
        public ProfileMapper()
        {
            ToTable("Profile");

            HasKey(t => t.Id);

            Property(t => t.FirstName).HasMaxLength(128);
            Property(t => t.MiddleName).HasMaxLength(128);
            Property(t => t.LastName).HasMaxLength(128);
            Property(t => t.ContactEmail).HasMaxLength(128);
            Property(t => t.City).HasMaxLength(128);
            Property(t => t.AdditionalInfo).HasMaxLength(1000);
            Property(t => t.BirthDate);
            Property(t => t.Gender).HasMaxLength(16);
            Property(t => t.RelationshipStatus).HasMaxLength(16);

            HasRequired(t => t.User).WithOptional(u => u.Profile);
        }
    }
}
