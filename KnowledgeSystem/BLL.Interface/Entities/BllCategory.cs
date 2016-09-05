using System.Collections.Generic;

namespace BLL.Interface
{
    /// <summary>
    /// BLL Layout Category class
    /// </summary>
    public class BllCategory
    {
        /// <summary>
        /// Create new BllCategory entity
        /// </summary>
        public BllCategory()
        {
            Skills = new List<BllSkill>();
        }
        /// <summary>
        /// BLL Category identify number
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// BLL Category Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// BllSkills collection, wich the category consists
        /// </summary>
        public List<BllSkill> Skills { get; set; }
    }
}
