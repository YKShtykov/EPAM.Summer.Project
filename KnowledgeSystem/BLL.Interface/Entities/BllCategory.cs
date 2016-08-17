using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public class BllCategory
    {
        public BllCategory()
        {
            Skills = new List<BllSkill>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<BllSkill> Skills { get; set; }
    }
}
