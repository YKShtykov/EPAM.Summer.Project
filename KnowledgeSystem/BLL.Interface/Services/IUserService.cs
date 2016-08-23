using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IUserService
    {
        void Create(BllUser user);
        void Update(BllUser user);
        BllUser Get(int id);
        void Delete(int id);
        IEnumerable<BllUser> GetAll();       
        BllUser Login(string emailOrLogin, string password);        

        void UpdateUserSkills(int userId, IDictionary<int, int> skillLevel);
        Dictionary<BllSkill,int> GetUserSkills(int userId);
    }
}
