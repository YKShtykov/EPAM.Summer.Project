using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface ISkillRepository: IRepository<DalSkill>
    {
        //IEnumerable<DalUserSkills> RateUsers(IEnumerable<string> sortings);
        Dictionary<DalSkill, int> GetUserSkillLevels(int userId);
        void UpdateUserSkillLevels(int userId, IDictionary<int, int> skillLevel);
        IEnumerable<DalUser> GetUsersWithThatSkill(DalSkill skill);
        int GetLevelOfSkill(int userId, int skillId);
    }
}
