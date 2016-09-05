using System.Collections.Generic;
using MvcApp.ViewModels;
using BLL.Interface;

namespace MvcApp.Infrastructure.Mappers
{
    public static class CategoryMapper
    {
        public static BllCategory Map(MvcCategory category)
        {
            BllCategory result = new BllCategory()
            {
                Id = category.Id,
                Name = category.Name
            };
            foreach (var skill in category.Skills)
            {
                result.Skills.Add(SkillMapper.Map(skill));
            }
            return result;
        }

        public static MvcCategory Map(BllCategory category)
        {
            MvcCategory result = new MvcCategory()
            {
                Id = category.Id,
                Name = category.Name
            };
            foreach (var skill in category.Skills)
            {
                result.Skills.Add(SkillMapper.Map(skill));
            }
            return result;
        }

        public static IEnumerable<MvcCategory> Map(IEnumerable<BllCategory> categories)
        {
            var result = new List<MvcCategory>();
            foreach (var item in categories)
            {
                result.Add(Map(item));
            }

            return result;
        }

        public static IEnumerable<BllCategory> Map(IEnumerable<MvcCategory> categories)
        {
            var result = new List<BllCategory>();
            foreach (var item in categories)
            {
                result.Add(Map(item));
            }

            return result;
        }
    }
}