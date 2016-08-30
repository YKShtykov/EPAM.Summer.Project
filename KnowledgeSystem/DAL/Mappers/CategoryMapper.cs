﻿using DAL.Interface;
using ORM;

namespace DAL.Mappers
{
    /// <summary>
    /// Service class for mapping DalCategory and ORM Category entities
    /// </summary>
    public static class CategoryMapper
    {
        /// <summary>
        /// Map Categories
        /// </summary>
        /// <param name="category"></param>
        /// <returns>new ORM Category same as category</returns>
        public static Category Map(DalCategory category)
        {
            Category result = new Category()
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
        /// Map Categories
        /// </summary>
        /// <param name="category"></param>
        /// <returns>new DalCategory same as category</returns>
        public static DalCategory Map(Category category)
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
    }
}
