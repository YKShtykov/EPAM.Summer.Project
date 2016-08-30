using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using ORM;

namespace DAL.Mappers
{
    public static class ProfileMapper
    {
        public static Profile Map(DalProfile profile)
        {
            return new Profile()
            {
                Id = profile.Id,
                LastName = profile.LastName,
                FirstName = profile.FirstName,
                MiddleName = profile.MiddleName,
                BirthDate = profile.BirthDate.ToString(),
                AdditionalInfo = profile.AdditionalInfo,
                City = profile.City,
                Gender = profile.Gender,
                Image = profile.Image,
                ImageMimeType = profile.ImageMimeType,
                RelationshipStatus = profile.RelationshipStatus
            };
        }

        public static DalProfile Map(Profile profile)
        {
            return new DalProfile()
            {
                Id = profile.Id,
                LastName = profile.LastName,
                FirstName = profile.FirstName,
                MiddleName = profile.MiddleName,
                BirthDate = Convert.ToDateTime(profile.BirthDate),
                AdditionalInfo = profile.AdditionalInfo,
                City = profile.City,
                Gender = profile.Gender,
                Image = profile.Image,
                ImageMimeType = profile.ImageMimeType,
                RelationshipStatus = profile.RelationshipStatus
            };
        }

        public static IEnumerable<DalProfile> Map(IQueryable<Profile> profiles)
        {
            var result = new List<DalProfile>();
            foreach (var item in profiles)
            {
                result.Add(Map(item));
            }

            return result;
        }
    }
}
