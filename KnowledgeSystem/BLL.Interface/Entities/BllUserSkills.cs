using System.Collections.Generic;

namespace BLL.Interface
{
    /// <summary>
    /// BLL Layout UserSkills class, wich stores short user information ant his skills and its levels.
    /// </summary>
    public class BllUserSkills
    {
        /// <summary>
        /// Create BllUserSkills entity
        /// </summary>
        public BllUserSkills()
        {
            Skills = new List<BllSkill>();
        }

        /// <summary>
        /// User Id
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
        /// Dictionary BllSkill-level
        /// </summary>
        public List<BllSkill> Skills { get; set; }
    }
}
