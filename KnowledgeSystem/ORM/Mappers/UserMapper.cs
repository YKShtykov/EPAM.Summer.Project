using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Mappers
{
    public class UserMapper: EntityTypeConfiguration<User>
    {
        public UserMapper()
        {
            ToTable("User");

            HasKey(t => t.Id);
            Property(t => t.Email).IsRequired().HasMaxLength(64);
            Property(t => t.Login).IsRequired().HasMaxLength(64);
            Property(t => t.Password).IsRequired().HasMaxLength(128);
            Property(t => t.PasswordSalt).IsRequired().HasMaxLength(128);

            HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(m => m.ToTable("UserRole")
                .MapLeftKey("UserId").MapRightKey("RoleId"));
        }
    }
}
