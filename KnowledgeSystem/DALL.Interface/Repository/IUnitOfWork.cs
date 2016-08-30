using System;

namespace DAL.Interface
{
    /// <summary>
    /// Interface for Unit of Work pattern implementation
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Category collection
        /// </summary>
        ICategoryRepository Categories { get; set; }
        /// <summary>
        /// User collection
        /// </summary>
        IUserRepository Users { get; set; }
        /// <summary>
        /// Skill collection
        /// </summary>
        ISkillRepository Skills { get; set; }
        /// <summary>
        /// Profile collection
        /// </summary>
        IProfileRepository Profiles { get; set; }

        /// <summary>
        /// The method for saving changes in collections
        /// </summary>
        void Commit();
    }
}
