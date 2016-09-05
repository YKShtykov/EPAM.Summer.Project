using System.Collections.Generic;

namespace DAL.Interface
{
    /// <summary>
    /// DAL Layout Category class
    /// </summary>
    public class DalCategory: IEntity
    {
        /// <summary>
        /// Creates DalCategory Entity
        /// </summary>
        public DalCategory()
        {
            Skills = new List<DalSkill>();
        }
        /// <summary>
        /// DAL Category identity number
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// DAL Category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// DAl Skills, which category has
        /// </summary>
        public List<DalSkill> Skills { get; set; }
    }
}
