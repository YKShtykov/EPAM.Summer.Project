using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DAL.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; set; }
        IUserRepository Users { get; set; }
        ISkillRepository Skills { get; set; }
        IProfileRepository Profiles { get; set; }

        void Commit();
    }
}
