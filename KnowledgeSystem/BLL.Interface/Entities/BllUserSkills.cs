using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public class BllUserSkills
    {
        public BllUserSkills()
        {
            SkillLevelPair = new Dictionary<BllSkill, int>();
        }

        public int userId { get; set; }
        public string UserLogin { get; set; }

        public Dictionary<BllSkill,int> SkillLevelPair { get; set; }
    }
}
