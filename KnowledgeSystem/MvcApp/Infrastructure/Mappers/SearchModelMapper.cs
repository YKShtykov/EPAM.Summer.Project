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
            var result = new BllSearchModel()
            {
                StringKey = model.StringKey,
                City = model.City,
                Gender = model.Gender
            };
            if (ReferenceEquals(model.Age,null))
            {
                result.Age = 10000;
            }
            else
            {
                result.Age = int.Parse(model.Age);
            }
            return result;
        }
    }
}