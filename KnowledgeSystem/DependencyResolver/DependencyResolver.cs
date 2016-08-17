using BLL;
using BLL.Interface;
using DAL;
using DAL.Interface;
using ORM;
using Ninject;
using Ninject.Web.Common;
using System.Data.Entity;


namespace DependencyResolver
{
    public static class DependencyResolver
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            Configure(kernel);
        }

        private static void Configure(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<DbContext>().To<KnowledgeSystemContext>().InRequestScope();

            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ISkillService>().To<SkillService>();            
            kernel.Bind<IProfileService>().To<ProfileService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();

            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<ISkillRepository>().To<SkillRepository>();
            kernel.Bind<IProfileRepository>().To<ProfileRepository>();
            kernel.Bind<ICategoryRepository>().To<CategoryRepository>();

        }
    }
}
