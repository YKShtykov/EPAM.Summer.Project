using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public class DalSkill: IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string CategoryName { get; set; }
    }
}
