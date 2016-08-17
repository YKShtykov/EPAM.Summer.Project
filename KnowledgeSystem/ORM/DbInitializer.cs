using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    class DbInitializer : CreateDatabaseIfNotExists<KnowledgeSystemContext>
    {
        protected override void Seed(KnowledgeSystemContext context)
        {
            context.Roles.AddRange(new Role[] { new Role() { Id=1, Name = "Administrator" },
                                                new Role() { Id=2, Name = "Manager" },
                                                new Role() { Id=3, Name = "User" }});

            context.SaveChanges();

            var adminRole = context.Set<Role>().FirstOrDefault(r => r.Id==1);

            User admin = new User()
            {
                Id = 1,
                Email = "Ifrit@mail.ru",
                Login = "admin",
                Password = AdminPasswordGenerator.Password,
                PasswordSalt = AdminPasswordGenerator.Salt        
            };

            admin.Roles.Add(adminRole);

            context.Set<User>().Add(admin);
            context.Set<Profile>().Add(new Profile() { Id = admin.Id, BirthDate = default(DateTime).ToString() });

            //context.Categories.Add(new Category() { Id = 1, Name = "Programming Languages"} );
            //context.Categories.Add(new Category() { Id = 2, Name = ".NET Frameworks"});
            //context.Categories.Add(new Category() { Id = 3, Name = "Additional Skills"});

            //var programingCategory = context.Categories.FirstOrDefault(c => c.Id == 1);
            //var frameworkCategory = context.Categories.FirstOrDefault(c => c.Id == 2);
            //var additionalCategory = context.Categories.FirstOrDefault(c => c.Id == 3);

            //context.Skills.AddRange(new Skill[] { new Skill() { Id=1, Name = "C", Category = programingCategory},
            //                                      new Skill() { Id=2, Name = "C++", Category = programingCategory },
            //                                      new Skill() { Id=3, Name = "C#", Category = programingCategory },
            //                                      new Skill() { Id=4, Name = "Java", Category = programingCategory },
            //                                      new Skill() { Id=5, Name = "PHP", Category = programingCategory },
            //                                      new Skill() { Id=6, Name = "Python", Category = programingCategory },
            //                                      new Skill() { Id=7, Name = "Entity Framework", Category= frameworkCategory },
            //                                      new Skill() { Id=8, Name = "ASP .NET", Category= frameworkCategory },
            //                                      new Skill() { Id=9, Name = "English Level", Category = additionalCategory },
            //                                      new Skill() { Id=10, Name = "Object Oriented Programming", Category = additionalCategory },
            //                                      new Skill() { Id=11, Name = "Information Security", Category = additionalCategory },
            //                                      new Skill() { Id=12, Name = "Computer Networks", Category = additionalCategory }});

            context.SaveChanges();
        }

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
