using DAL.Interface;
using ORM;

namespace DAL.Mappers
{
    /// <summary>
    /// Service class for mapping DalRole and ORM Role entities
    /// </summary>
    public static class RoleMapper
    {
        /// <summary>
        /// Map Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns>new ORM Role same as role</returns>
        public static Role MapRole(DalRole role)
        {
            return new Role()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        /// <summary>
        /// Map Role
        /// </summary>
        /// <param name="role"></param>
        /// <returns>new DalRole same as role</returns>
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
