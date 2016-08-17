using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using ORM;

namespace DAL.Mappers
{
    public static class RoleMapper
    {
        public static Role MapRole(DalRole role)
        {
            return new Role()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public static DalRole MapRole(Role role)
        {
            return new DalRole()
            {
                Id = role.Id,
                Name = role.Name
            };
        }
    }
}
