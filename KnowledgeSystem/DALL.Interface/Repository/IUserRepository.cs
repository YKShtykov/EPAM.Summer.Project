using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IUserRepository : IRepository<DalUser>
    {
        DalUser LoginUser(string emailOrLogin);
        //bool ConsistEmail(string key);
        //bool ConsistLogin(string key);
        //int GetSkillLevel(int userId, int skillId);
        //void UpdateSkillLevel(int userId, int skillId, int level);
        //void UpdateAllSkillLevels(int userId,IDictionary<int,int> skillLevel);
        //Dictionary<DalSkill, int> GetAllSkillLevels(int userId);
    }
}
