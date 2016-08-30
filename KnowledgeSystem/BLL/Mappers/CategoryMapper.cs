using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL.Interface;

namespace BLL.Mappers
{
    public static class CategoryMapper
    {
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
