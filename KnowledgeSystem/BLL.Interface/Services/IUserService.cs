using System.Collections.Generic;

namespace BLL.Interface
{
    /// <summary>
    /// Interface for work with users 
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// The method for creating new BllUser
        /// </summary>
        /// <param name="user"></param>
        void Create(BllUser user);
        /// <summary>
        /// The method for updating exsisting BllUser
        /// </summary>
        /// <param name="user"></param>
        void Update(BllUser user);
        /// <summary>
        /// The method for deleting BllUser
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
        /// <summary>
        /// The method for getting BllUser by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BllUser</returns>
        BllUser Get(int id);
        /// <summary>
        /// The method for getting all BllUsers
        /// </summary>
        /// <returns>BllUser collection</returns>
        IEnumerable<BllUser> GetAll();       
        /// <summary>
        /// The method for user logining
        /// </summary>
        /// <param name="emailOrLogin"></param>
        /// <param name="password"></param>
        /// <returns>BllUser</returns>
        BllUser Login(string emailOrLogin, string password);
        /// <summary>
        /// The method for updating users skills
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="categories"></param>
        void UpdateUserSkills(int userId, IEnumerable<BllCategory> categories);
        /// <summary>
        /// The method for getting user skills
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>dictionary BllSkill-level</returns>
        IEnumerable<BllCategory> GetUserSkills(int userId);

        /// <summary>
        /// The method for getting user sorted skills
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skillsWithNullLevel"></param>
        /// <returns></returns>
        IEnumerable<BllCategory> GetSortedUserSkills(int userId, bool skillsWithNullLevel);
    }
}
