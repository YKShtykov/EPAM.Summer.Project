using System.Data.Entity.ModelConfiguration;

namespace ORM.Mappers
{
    /// <summary>
    /// Service class for configurating Category table
    /// </summary>
    class CategoryMapper: EntityTypeConfiguration<Category>
    {
        public CategoryMapper()
        {
            ToTable("Category");

            HasKey(t => t.Id);
            Property(t => t.Name).IsRequired().HasMaxLength(32);

            HasMany(t => t.Skills);
        }
    }
}
