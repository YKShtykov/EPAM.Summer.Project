using System.Data.Entity;
using System.Linq;
using CryptoService.Interface;

namespace ORM
{
    /// <summary>
    /// Service class for actions, when DataBase is created
    /// </summary>
    class DbInitializer : CreateDatabaseIfNotExists<KnowledgeSystemContext>
    {
        private readonly IPasswordService passwordService;

        public DbInitializer(IPasswordService passwordService)
        {
            this.passwordService = passwordService;
        }

        /// <summary>
        /// Method creates all Roles, and initial Administrator
        /// </summary>
        /// <param name="context">DbContext</param>
        protected override void Seed(KnowledgeSystemContext context)
        {
            context.Roles.AddRange(new Role[] { new Role() { Id=1, Name = "Administrator" },
                                                new Role() { Id=2, Name = "Manager" },
                                                new Role() { Id=3, Name = "User" }});

            context.SaveChanges();

            User admin = new User()
            {
                Id = 1,
                Email = "Ifrit@mail.ru",
                Login = "admin",
                PasswordSalt = passwordService.Key,
            };

            admin.Password = passwordService.GetHash("123456", admin.PasswordSalt);

            var adminRoles = context.Set<Role>().Select(r => r);
            foreach (var item in adminRoles)
            {
                admin.Roles.Add(item);
            }

            context.Set<User>().Add(admin);
            context.Set<Profile>().Add(new Profile() { Id = admin.Id });
            context.SaveChanges();
        }
    }
}
