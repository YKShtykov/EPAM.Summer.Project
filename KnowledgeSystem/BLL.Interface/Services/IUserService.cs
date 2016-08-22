using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IUserService
    {
        BllUser GetBllUser(int id);
        IEnumerable<BllUser> GetAllBllUsers();
        void CreateUser(BllUser user);
        BllUser LoginUser(string emailOrLogin, string password);
        void DeleteUser(int id);
        void UpdateUser(BllUser user);
        int GetSkillLevel(int userId, int skillId);
        void UpdateSkillLevel(int userId, int skillId, int level);
        void UpdateAllSkillLevels(int userId, IDictionary<int, int> skillLevel);

        Dictionary<BllSkill,int> GetAllSkillLevels(int userId);
    }
}
