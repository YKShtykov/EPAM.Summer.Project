using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Skill
    {
        public Skill()
        {
            UsersSkills = new List<UserSkill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public Category Category { get; set; }
        public ICollection<UserSkill> UsersSkills { get; set; }
    }
}
