using System.Collections.Generic;

namespace ORM
{
    /// <summary>
    /// ORM Layout Category Class
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Create ORM Category entity
        /// </summary>
        public Category()
        {
            Skills = new List<Skill>();
        }

        /// <summary>
        /// Category Identify number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Skills, wich the category consists. It need for DataBase creating
        /// </summary>
        public ICollection<Skill> Skills { get; set; }
    }
}
