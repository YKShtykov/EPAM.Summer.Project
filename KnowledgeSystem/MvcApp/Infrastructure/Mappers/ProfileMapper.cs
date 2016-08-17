﻿using System;
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
        public static BllProfile MapProfile(MvcProfile profile)
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
                Gender = Enum.GetName(typeof(Gender), profile.Gender),
                RelationshipStatus = Enum.GetName(typeof(RelationshipStatus), profile.RelationshipStatus)
            };
        }

        public static MvcProfile MapProfile(BllProfile profile)
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
                //Gender = profile.Gender,
                //RelationshipStatus = Enum.GetName(typeof(RelationshipStatus), profile.RelationshipStatus)
            };
        }
    }
}