namespace DAL.Interface
{
    /// <summary>
    /// DAL Layout Skill class
    /// </summary>
    public class DalSkill: IEntity
    {
        /// <summary>
        /// DAL Skill identify number
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// DAL Skill name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User skill level
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Category, wich has the skill
        /// </summary>
        public string CategoryName { get; set; }
    }
}
