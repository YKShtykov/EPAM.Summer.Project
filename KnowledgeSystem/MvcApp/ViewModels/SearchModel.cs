namespace MvcApp.ViewModels
{
    /// <summary>
    /// Class model for user searching
    /// </summary>
    public class SearchModel
    {
        /// <summary>
        /// Part of user name
        /// </summary>
        public string StringKey { get; set; }        
        /// <summary>
        /// User city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Finded users
        /// </summary>
        public GenericPaginationModel<MvcProfile> Profiles { get; set; }
    }
}