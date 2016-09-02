using System.Collections.Generic;
using BLL.Interface;
using DAL.Interface;

namespace BLL.Mappers
{
    /// <summary>
    /// Service class for mapping BllProfile and DalProfile
    /// </summary>
    public static class ProfileMapper
    {
        /// <summary>
        /// Map Profile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns>new BllProfile same as profile</returns>
        public static BllProfile Map(DalProfile profile)
        {
            return new BllProfile()
            {
                Id = profile.Id,
                LastName = profile.LastName,
                FirstName = profile.FirstName,
                MiddleName = profile.MiddleName,
                ContactEmail = profile.ContactEmail,
                ContactPhone = profile.ContactPhone,
                BirthDate = profile.BirthDate,
                AdditionalInfo = profile.AdditionalInfo,
                City = profile.City,
                Gender = profile.Gender,
                RelationshipStatus = profile.RelationshipStatus,
                Image = profile.Image,
                ImageMimeType=profile.ImageMimeType                
            };
        }

        /// <summary>
        /// Map profile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns>new DalProfile same as profile</returns>
        public static DalProfile Map(BllProfile profile)
        {
            return new DalProfile()
            {
                Id = profile.Id,
                LastName = profile.LastName,
                FirstName = profile.FirstName,
                MiddleName = profile.MiddleName,
                ContactEmail = profile.ContactEmail,
                ContactPhone = profile.ContactPhone,
                BirthDate = profile.BirthDate,
                AdditionalInfo = profile.AdditionalInfo,
                City = profile.City,
                Gender = profile.Gender,
                RelationshipStatus = profile.RelationshipStatus,
                Image = profile.Image,
                ImageMimeType = profile.ImageMimeType
            };
        }

        /// <summary>
        /// Map profiles
        /// </summary>
        /// <param name="profiles"></param>
        /// <returns>BllProfiles collection same as profiles</returns>
        public static IEnumerable<BllProfile> Map(IEnumerable<DalProfile> profiles)
        {
            var result = new List<BllProfile>();
            foreach (var item in profiles)
            {
                result.Add(Map(item));
            }

            return result;
        }
    }
}
