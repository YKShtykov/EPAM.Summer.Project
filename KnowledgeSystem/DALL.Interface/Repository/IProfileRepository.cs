namespace DAL.Interface
{
    /// <summary>
    /// Interface for IRepository addons for Profile collection
    /// </summary>
    public interface IProfileRepository: IRepository<DalProfile>
    {
        /// <summary>
        /// The method for creating profile by User Id
        /// </summary>
        /// <param name="id"></param>
        void Create(int id);
    }
}
