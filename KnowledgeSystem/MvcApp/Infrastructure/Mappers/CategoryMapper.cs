using System.Collections.Generic;
using MvcApp.ViewModels;
using BLL.Interface;

namespace MvcApp.Infrastructure.Mappers
{
    /// <summary>
    /// Class-mapper for categories
    /// </summary>
    public static class CategoryMapper
    {
        /// <summary>
        /// Map category
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Bll category such as category</returns>
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

        /// <summary>
        /// Map category
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Mvc category such as category</returns>
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

        /// <summary>
        /// Map category list
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        public static IEnumerable<MvcCategory> Map(IEnumerable<BllCategory> categories)
        {
            var result = new List<MvcCategory>();
            foreach (var item in categories)
            {
                result.Add(Map(item));
            }

            return result;
        }

        /// <summary>
        /// Map category list
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
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