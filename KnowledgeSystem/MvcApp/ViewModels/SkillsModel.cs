using System.Collections.Generic;

namespace MvcApp.ViewModels
{
    /// <summary>
    /// Class model for user skills
    /// </summary>
    public class SkillsModel
    {
        /// <summary>
        /// Create skill model class
        /// </summary>
        public SkillsModel()
        {
            Skills = new List<MvcSkill>();      
        }
        /// <summary>
        /// User identify number
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// User last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// User photo
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// User skills
        /// </summary>
        public List<MvcSkill> Skills { get; set; }
    }
}