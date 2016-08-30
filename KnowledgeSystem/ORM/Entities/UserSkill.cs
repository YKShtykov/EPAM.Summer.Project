using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORM
{
    /// <summary>
    /// Servise class UserSkill. It need for creating table in DataBase, wich stores users levels of skills 
    /// </summary>
    public class UserSkill
    {
        /// <summary>
        /// ORM User identify number
        /// </summary>
        [Key, Column(Order = 0)]
        public int UserId { get; set; }

        /// <summary>
        /// ORM Skill identify number
        /// </summary>
        [Key, Column(Order = 1)]
        public int SkillId { get; set; }

        /// <summary>
        /// ORM User, whose level skill is stored in table
        /// </summary>
        public virtual User User { get; set; }
        /// <summary>
        /// ORM Skill, that ORM user has
        /// </summary>
        public virtual Skill Skill { get; set; }

        /// <summary>
        /// Level of skill
        /// </summary>
        public int Level { get; set; }
    }
}
