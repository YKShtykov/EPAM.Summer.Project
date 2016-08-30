using System.Collections.Generic;

namespace BLL.Interface
{
    /// <summary>
    /// Interface for work with profiles
    /// </summary>
    public interface IProfileService
    {
        /// <summary>
        /// The method for getting profile by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BllProfile</returns>
        BllProfile Get(int id);
        /// <summary>
        /// The method for updating exsisting BllProfile
        /// </summary>
        /// <param name="profile"></param>
        void Update(BllProfile profile);
        /// <summary>
        /// The method for BllProfiles search
        /// </summary>
        /// <param name="model"></param>
        /// <returns>BllProfile collection</returns>
        IEnumerable<BllProfile> Search(BllSearchModel model);
    }
}
