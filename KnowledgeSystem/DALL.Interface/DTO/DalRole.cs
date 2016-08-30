namespace DAL.Interface
{
    /// <summary>
    /// DAL Layout Role class
    /// </summary>
    public class DalRole:IEntity
    {
        /// <summary>
        /// DAL Role identify number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// DAL Role name
        /// </summary>
        public string Name { get; set; }
    }
}
