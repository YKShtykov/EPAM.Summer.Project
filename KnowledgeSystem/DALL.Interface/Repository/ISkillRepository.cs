using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ISkillRepository: IRepository<DalSkill>
    {
        IEnumerable<DalUserSkills> RateUsers(IEnumerable<string> sortings);
    }
}
