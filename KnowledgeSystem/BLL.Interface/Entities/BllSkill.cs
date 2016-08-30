namespace BLL.Interface
{
    /// <summary>
    /// BLL Layout Skill class
    /// </summary>
    public class BllSkill
    {
        /// <summary>
        /// BLL Skill identify number
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// BLL Skill name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Category name wich consists the skill
        /// </summary>
        public string CategoryName { get; set; }
    }
}
