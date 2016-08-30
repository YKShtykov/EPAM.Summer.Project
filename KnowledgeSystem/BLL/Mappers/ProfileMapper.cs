using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL.Interface;

namespace BLL.Mappers
{
    public static class ProfileMapper
    {
        public static BllProfile Map(DalProfile profile)
        {
            return new BllProfile()
            {
                Id = profile.Id,
                LastName = profile.LastName,
                FirstName = profile.FirstName,
                MiddleName = profile.MiddleName,
                BirthDate = profile.BirthDate,
                Age = profile.Age,
                AdditionalInfo = profile.AdditionalInfo,
                City = profile.City,
                Gender = profile.Gender,
                RelationshipStatus = profile.RelationshipStatus,
                Image = profile.Image,
                ImageMimeType=profile.ImageMimeType                
            };
        }

        public static DalProfile Map(BllProfile profile)
        {
            return new DalProfile()
            {
                Id = profile.Id,
                LastName = profile.LastName,
                FirstName = profile.FirstName,
                MiddleName = profile.MiddleName,
                BirthDate = profile.BirthDate,
                Age = profile.Age,
                AdditionalInfo = profile.AdditionalInfo,
                City = profile.City,
                Gender = profile.Gender,
                RelationshipStatus = profile.RelationshipStatus,
                Image = profile.Image,
                ImageMimeType = profile.ImageMimeType
            };
        }
    }
}
