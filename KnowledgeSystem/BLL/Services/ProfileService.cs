using System;
using System.Collections.Generic;
using BLL.Interface;
using DAL.Interface;
using BLL.Mappers;
using System.Linq;

namespace BLL
{
    /// <summary>
    /// Service for fork with BllProfiles
    /// </summary>
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork uow;

        /// <summary>
        /// Create new ProfileService instance
        /// </summary>
        /// <param name="uow"></param>
        public ProfileService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        /// <summary>
        /// The method for getting profile by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>BllProfile</returns>
        public BllProfile Get(int id)
        {
            var profile = ProfileMapper.Map(uow.Profiles.Get(id));
            profile.Age = GetAge(profile.BirthDate);

            return profile;
        }

        /// <summary>
        /// The method for updating existing profile
        /// </summary>
        /// <param name="profile"></param>
        public void Update(BllProfile profile)
        {
            uow.Profiles.Update(ProfileMapper.Map(profile));
            uow.Commit();
        }

        /// <summary>
        /// The method for profile searching
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IEnumerable<BllProfile> Find(string stringKey = "", string city = null)
        {
            var profiles = uow.Profiles.GetAll();
            if (!ReferenceEquals(stringKey, null)) profiles = profiles.Where(p => (p.FirstName + " " + p.LastName).Contains(stringKey));
            if (!ReferenceEquals(city, null)) profiles = profiles.Where(p => p.City!=null&&p.City.Contains(city));

            return ProfileMapper.Map(profiles);
        }

        /// <summary>
        /// The method for calculating age by birth date
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns>age</returns>
        private int? GetAge(DateTime? birthDate)
        {
            if (ReferenceEquals(birthDate, null))
                return null;

            DateTime today = DateTime.Today;
            int age = today.Year - ((DateTime)birthDate).Year;
            if (birthDate > today.AddYears(-age)) age--;

            return age;
        }
    }
}
