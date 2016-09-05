using System.Collections.Generic;

namespace DAL.Interface
{
    /// <summary>
    /// DAL LAyout service class, wich stores short user information ant his skills and its levels.
    /// </summary>
    public class DalUserSkills
    {
        /// <summary>
        /// Create DalUSerSkills entity
        /// </summary>
        public DalUserSkills()
        {
            Skills = new List<DalSkill>();
        }
        /// <summary>
        /// DAL User identify number
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// User login
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User login
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User photo
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// Dictionary storing skill - key and its level - value
        /// </summary>
        public List<DalSkill> Skills { get; set; }
    }
}
