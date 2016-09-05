using System.Collections.Generic;

namespace DAL.Interface
{
    /// <summary>
    /// Interface for IRepository addons Skills collection
    /// </summary>
    public interface ISkillRepository: IRepository<DalSkill>
    {
        /// <summary>
        /// The method for getting all user skills by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Dictionary DalSkill-level</returns>
        List<DalSkill> GetUserSkills(int userId);
        /// <summary>
        /// The method for updating all user skills
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skillLevel"></param>
        void UpdateUserSkills(int userId, IDictionary<int, int> skillLevel);

        /// <summary>
        /// The method for updating level of skill
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skillId"></param>
        /// <param name="level"></param>
        void UpdateSkillLevel(int userId, int skillId, int level);
        /// <summary>
        /// The method for getting all users with that skill
        /// </summary>
        /// <param name="skill"></param>
        /// <returns> DalUser collection </returns>
        IEnumerable<DalUser> GetUsersWithThatSkill(DalSkill skill);
        /// <summary>
        /// The method for getting user level in that skill by user Id and skill Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="skillId"></param>
        /// <returns>Level of skill</returns>
        int GetLevelOfSkill(int userId, int skillId);

        List<DalCategory> GetSortedUserSkills(int userId);
    }
}
