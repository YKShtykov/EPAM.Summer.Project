﻿using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interface;
using ORM;

namespace DAL.Mappers
{
    /// <summary>
    /// Service class for mapping DalProfile and ORM Profile entities
    /// </summary>
    public static class ProfileMapper
    {
        /// <summary>
        /// Map Profile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns>new ORM Profile same as profile</returns>
        public static Profile Map(DalProfile profile)
        {
            return new Profile()
            {
                Id = profile.Id,
                LastName = profile.LastName,
                FirstName = profile.FirstName,
                MiddleName = profile.MiddleName,
                ContactEmail = profile.ContactEmail,
                ContactPhone = profile.ContactPhone,
                BirthDate = profile.BirthDate.ToString(),
                AdditionalInfo = profile.AdditionalInfo,
                City = profile.City,
                Gender = profile.Gender,
                Image = profile.Image,
                ImageMimeType = profile.ImageMimeType,
                RelationshipStatus = profile.RelationshipStatus
            };
        }

        /// <summary>
        /// Map profile
        /// </summary>
        /// <param name="profile"></param>
        /// <returns>new DalProfile same as profile</returns>
        public static DalProfile Map(Profile profile)
        {
            var birthDate = new DateTime?();
            if (ReferenceEquals(profile.BirthDate,null)||profile.BirthDate=="")
            {
                birthDate = null;
            }
            else
            {
                birthDate = Convert.ToDateTime(profile.BirthDate);
            }
            return new DalProfile()
            {
                Id = profile.Id,
                LastName = profile.LastName,
                FirstName = profile.FirstName,
                MiddleName = profile.MiddleName,
                ContactEmail = profile.ContactEmail,
                ContactPhone = profile.ContactPhone,
                BirthDate = birthDate,
                AdditionalInfo = profile.AdditionalInfo,
                City = profile.City,
                Gender = profile.Gender,
                Image = profile.Image,
                ImageMimeType = profile.ImageMimeType,
                RelationshipStatus = profile.RelationshipStatus
            };
        }

        /// <summary>
        /// Map profiles
        /// </summary>
        /// <param name="profiles"></param>
        /// <returns>new collection of DalProffiles sames as profiles</returns>
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
