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
            SkillLevelPair = new Dictionary<DalSkill, int>();
        }
        /// <summary>
        /// DAL User identify number
        /// </summary>
        public int userId { get; set; }
        /// <summary>
        /// DAL USer login
        /// </summary>
        public string UserLogin { get; set; }

        /// <summary>
        /// Dictionary storing skill - key and its level - value
        /// </summary>
        public Dictionary<DalSkill, int> SkillLevelPair { get; set; }
    }
}
