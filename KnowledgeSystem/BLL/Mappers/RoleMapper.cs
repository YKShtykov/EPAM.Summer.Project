using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL.Interface;

namespace BLL.Mappers
{
    public static class RoleMapper
    {
        public static BllRole MapRole(DalRole role)
        {
            return new BllRole()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static DalRole MapRole(BllRole role)
        {
            return new DalRole()
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}
