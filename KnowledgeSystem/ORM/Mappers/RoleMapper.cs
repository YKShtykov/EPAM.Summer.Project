using System.Data.Entity.ModelConfiguration;

namespace ORM.Mappers
{
    /// <summary>
    /// Service class for configurating Role table
    /// </summary>
    class RoleMapper : EntityTypeConfiguration<Role>
    {
        public RoleMapper()
        {
            ToTable("Role");

            HasKey(t => t.Id);

            Property(t => t.Name).IsRequired().HasMaxLength(32);
        }
    }
}
