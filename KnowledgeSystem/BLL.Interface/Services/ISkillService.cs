using System.Collections.Generic;

namespace BLL.Interface
{
    public interface ISkillService
    {
        void Create(BllSkill skill);
        void Update(BllSkill skill);
        void Delete(int id);
        BllSkill Get(int id);
        IEnumerable<BllSkill> GetAll();       
        IEnumerable<BllUserSkills> RateUsers(IEnumerable<string> sortings);   
    }
}
