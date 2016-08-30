using System.Collections.Generic;
using BLL.Interface;
using DAL.Interface;

namespace BLL.Mappers
{
    /// <summary>
    /// Service class for mapping DalCategory and BllCategory entities
    /// </summary>
    public static class CategoryMapper
    {
        /// <summary>
        /// Map category
        /// </summary>
        /// <param name="category"></param>
        /// <returns>new Bll category same as category</returns>
        public static BllCategory Map(DalCategory category)
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
        /// Mapcategory
        /// </summary>
        /// <param name="category"></param>
        /// <returns>new DalCategory same as category</returns>
        public static DalCategory Map(BllCategory category)
        {
            DalCategory result = new DalCategory()
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
        /// Map categories
        /// </summary>
        /// <param name="categories"></param>
        /// <returns>BllCategories collection same as categories</returns>
        public static IEnumerable<BllCategory> Map(IEnumerable<DalCategory> categories)
        {
            List<BllCategory> result = new List<BllCategory>();
            foreach (var item in categories)
            {
                result.Add(Map(item));
            }

            return result;
        }
    }
}
