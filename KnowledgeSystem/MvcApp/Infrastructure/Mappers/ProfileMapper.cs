using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcApp.ViewModels;
using BLL.Interface;
using MvcApp.Infrastructure.Mappers;

namespace MvcApp.Infrastructure.Mappers
{
    public static class ProfileMapper
    {
        public static BllProfile Map(MvcProfile profile)
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
                Image = profile.Image,
                ImageMimeType = profile.ImageMimeType,                
                Gender = profile.Gender,
                RelationshipStatus = profile.RelationshipStatus
            };
        }

        public static MvcProfile Map(BllProfile profile)
        {
            return new MvcProfile()
            {
                Id = profile.Id,
                LastName = profile.LastName,
                FirstName = profile.FirstName,
                MiddleName = profile.MiddleName,
                BirthDate = profile.BirthDate,
                Age = profile.Age,
                AdditionalInfo = profile.AdditionalInfo,
                City = profile.City,
                Image = profile.Image,
                ImageMimeType = profile.ImageMimeType,
                Gender = profile.Gender,
                RelationshipStatus =profile.RelationshipStatus
            };
        }

        public static IEnumerable<MvcProfile> Map(IEnumerable<BllProfile> profiles)
        {
            var result = new List<MvcProfile>();
            foreach (var item in profiles)
            {
                result.Add(Map(item));
            }

            return result;
        }
    }
}