using BLL.Interface;
using DAL.Interface;

namespace BLL.Mappers
{
    /// <summary>
    /// Service class for mapping BllRole and DalRole entities
    /// </summary>
    public static class RoleMapper
    {
        /// <summary>
        /// Map Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns>new BllRole same as role</returns>
        public static BllRole MapRole(DalRole role)
        {
            return new BllRole()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        /// <summary>
        /// map Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns>new DalRole same as role</returns>
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
