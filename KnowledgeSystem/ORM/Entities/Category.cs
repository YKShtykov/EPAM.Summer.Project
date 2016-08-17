using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class Category
    {
        public Category()
        {
            Skills = new List<Skill>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Skill> Skills { get; set; }
    }
}
