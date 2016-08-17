using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public class DalCategory: IEntity
    {
        public DalCategory()
        {
            Skills = new List<DalSkill>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<DalSkill> Skills { get; set; }
    }
}
