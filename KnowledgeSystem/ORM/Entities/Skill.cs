using System.Collections.Generic;

namespace ORM
{
    /// <summary>
    /// ORM Layout skill class
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// Create ORM Skill entity
        /// </summary>
        public Skill()
        {
            UsersSkills = new List<UserSkill>();
        }

        /// <summary>
        /// Skill identify number
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Skill name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ORM Category, wich has the skill. It need for DataBase creating
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// ORM UserSkill class, wich stores user level in the skill. It need for DataBase creating
        /// </summary>
        public ICollection<UserSkill> UsersSkills { get; set; }
    }
}
