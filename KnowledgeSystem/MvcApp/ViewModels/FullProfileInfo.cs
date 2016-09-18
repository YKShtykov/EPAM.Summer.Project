namespace MvcApp.ViewModels
{
    /// <summary>
    /// Class consists user profile info and skills
    /// </summary>
    public class FullProfileInfo
    {
        /// <summary>
        /// User profile
        /// </summary>
        public MvcProfile Profile { get; set; }
        /// <summary>
        /// User skills
        /// </summary>
        public GenericPaginationModel<MvcCategory> Categories { get; set; }
    }
}