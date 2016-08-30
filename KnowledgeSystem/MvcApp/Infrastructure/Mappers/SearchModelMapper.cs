using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface;
using MvcApp.ViewModels;

namespace MvcApp.Infrastructure.Mappers
{
    public class SearchModelMapper
    {
        public static BllSearchModel Map(SearchModel model)
        {
            return new BllSearchModel()
            {
                StringKey = model.StringKey,
                Age = model.Age,
                City = model.City,
                Gender = model.Gender
            };
        }
    }
}