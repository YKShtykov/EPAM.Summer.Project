using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace ORM
{
    /// <summary>
    /// Service class for actions, when DataBase is created
    /// </summary>
    class DbInitializer : CreateDatabaseIfNotExists<KnowledgeSystemContext>
    {
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

            var adminRoles = context.Set<Role>().Select(r=>r);

            User admin = new User()
            {
                Id = 1,
                Email = "Ifrit@mail.ru",
                Login = "admin",
                Password = AdminPasswordGenerator.Password,
                PasswordSalt = AdminPasswordGenerator.Salt        
            };

            foreach (var item in adminRoles)
            {
                admin.Roles.Add(item);
            }            

            context.Set<User>().Add(admin);
            context.Set<Profile>().Add(new Profile() { Id = admin.Id, BirthDate = default(DateTime).ToString() });
            context.SaveChanges();
        }

        /// <summary>
        /// The mehod for creating hashed administrator password
        /// </summary>
        internal static class AdminPasswordGenerator
        {
            internal static string Salt { get; }
            internal static string Password { get; }

            static AdminPasswordGenerator()
            {
                var cryptoService = new RNGCryptoServiceProvider();
                var saltBytes = new byte[128];
                cryptoService.GetNonZeroBytes(saltBytes);
                Salt = Encoding.Unicode.GetString(saltBytes);

                var bytes = Encoding.Unicode.GetBytes("123456" + Salt);
                var hashed = MD5.Create().ComputeHash(bytes);
                Password = Encoding.Unicode.GetString(hashed);
            }
        }

    }
}
