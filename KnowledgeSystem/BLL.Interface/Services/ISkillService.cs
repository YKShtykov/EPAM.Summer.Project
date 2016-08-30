using System.Collections.Generic;

namespace BLL.Interface
{
    /// <summary>
    /// Interface for work with skills
    /// </summary>
    public interface ISkillService
    {
        /// <summary>
        /// The method for creating new BllSkill
        /// </summary>
        /// <param name="skill"></param>
        void Create(BllSkill skill);
        /// <summary>
        /// The method for updating exsisting BllSkill
        /// </summary>
        /// <param name="skill"></param>
        void Update(BllSkill skill);
        /// <summary>
        /// The method for deleting BllSkill
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);
        /// <summary>
        /// The method for getting BllSkill by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BllSkill Get(int id);
        /// <summary>
        /// The method for getting all BllSkills
        /// </summary>
        /// <returns></returns>
        IEnumerable<BllSkill> GetAll();       
        /// <summary>
        /// The method for construct rating of users
        /// </summary>
        /// <param name="sortings"></param>
        /// <returns>BllUserSkills collection</returns>
        IEnumerable<BllUserSkills> RateUsers(IEnumerable<string> sortings);   
    }
}
