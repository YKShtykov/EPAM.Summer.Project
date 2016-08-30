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
            SkillLevelPair = new Dictionary<BllSkill, int>();
        }

        /// <summary>
        /// User Id
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// User login
        /// </summary>
        public string UserLogin { get; set; }

        /// <summary>
        /// Dictionary BllSkill-level
        /// </summary>
        public Dictionary<BllSkill,int> SkillLevelPair { get; set; }
    }
}
