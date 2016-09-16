using DAL.Interface;
using ORM;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Mappers
{
    /// <summary>
    /// Service class for mapping DalRole and ORM Role entities
    /// </summary>
    public static class RoleMapper
    {
        /// <summary>
        /// The method maps strings collection to roles collection
        /// </summary>
        /// <param name="roles"></param>
        /// <returns>Roles collection</returns>
        public static ICollection<Role> Map(ICollection<string> roles)
        {
            var result = new List<Role>();
            using (KnowledgeSystemContext knowledgeContext = new KnowledgeSystemContext())
            {
                foreach (var role in roles)
                {
                    var userRole = knowledgeContext.Set<Role>().FirstOrDefault(r => r.Name == role);
                    result.Add(userRole);
                }
            }
            return result;
        }
    }
}
