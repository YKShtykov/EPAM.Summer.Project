using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface ISkillService
    {
        BllSkill GetById(int id);
        IEnumerable<BllSkill> GetAll();
        void Create(BllSkill skill);
        void Delete(int id);
        void Update(BllSkill skill);       
    }
}
