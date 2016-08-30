namespace BLL.Interface
{
    /// <summary>
    /// Service class for search by users.Includes several parameters
    /// </summary>
    public class BllSearchModel
    {
        /// <summary>
        /// Maximum Age
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// String, wich user name must has
        /// </summary>
        public string StringKey { get; set; }
        /// <summary>
        /// User gender
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// User city
        /// </summary>
        public string City { get; set; }
    }
}
