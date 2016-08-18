using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public class DalUserSkills
    {
        public DalUserSkills()
        {
            SkillLevelPair = new Dictionary<DalSkill, int>();
        }
        public int userId { get; set; }
        public string UserLogin { get; set; }

        public Dictionary<DalSkill, int> SkillLevelPair { get; set; }
    }
}
