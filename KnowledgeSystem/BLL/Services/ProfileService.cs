using System;
using System.Collections.Generic;
using BLL.Interface;
using DAL.Interface;
using BLL.Mappers;

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
        public IEnumerable<BllProfile> Search(BllSearchModel model)
        {
            var result = new List<BllProfile>();

            var profiles = ProfileMapper.Map(uow.Profiles.GetAll());

            foreach (var item in profiles)
            {
                if (!ReferenceEquals(model.StringKey,null))
                {
                    string fullName = item.FirstName + " " + item.LastName;
                    if (!fullName.Contains(model.StringKey)) break;
                }
                item.Age = GetAge(item.BirthDate);
                if (model.Age!=0)
                {                    
                    if (item.Age > model.Age) break;
                }
                if (!ReferenceEquals(model.City,null))
                {
                    if (item.City != model.City) break;
                }
                if (model.Gender!= "Unspecified")
                {
                    if (item.Gender != model.Gender) break;
                }
                result.Add(item);
            }

            return result;
        }

        /// <summary>
        /// The method for calculating age by birth date
        /// </summary>
        /// <param name="birthDate"></param>
        /// <returns>age</returns>
        private int GetAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;

            return age;
        }
    }
}
