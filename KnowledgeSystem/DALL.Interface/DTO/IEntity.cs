namespace DAL.Interface
{
    /// <summary>
    /// DAL Layout entity interface. Each entity mast have identify number
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Entity identify number
        /// </summary>
        int Id { get; set; }
    }
}
