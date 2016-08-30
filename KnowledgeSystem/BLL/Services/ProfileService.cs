using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using DAL.Interface;
using BLL.Mappers;

namespace BLL
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork uow;

        public ProfileService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public BllProfile Get(int id)
        {
            var profile = uow.Profiles.Get(id);
            profile.Age = GetAge(profile.BirthDate);

            return ProfileMapper.Map(profile);
        }

        public void Update(BllProfile profile)
        {
            uow.Profiles.Update(ProfileMapper.Map(profile));
            uow.Commit();
        }

        public IEnumerable<BllProfile> Search(BllSearchModel model)
        {
            var result = new List<BllProfile>();

            var profiles = uow.Profiles.GetAll();
            foreach (var item in profiles)
            {
                if (!ReferenceEquals(model.StringKey,null))
                {
                    string fullName = item.FirstName + " " + item.LastName;
                    if (!fullName.Contains(model.StringKey)) break;
                }
                item.Age = GetAge(item.BirthDate);
                if (model.Age!=0)
                {                    
                    if (item.Age > model.Age) break;
                }
                if (!ReferenceEquals(model.City,null))
                {
                    if (item.City != model.City) break;
                }
                if (model.Gender!= "Unspecified")
                {
                    if (item.Gender != model.Gender) break;
                }
                result.Add(ProfileMapper.Map(item));
            }

            return result;
        }

        private int GetAge(DateTime birthDate)
        {
            DateTime today = DateTime.Today;
            int age = today.Year - birthDate.Year;
            if (birthDate > today.AddYears(-age)) age--;

            return age;
        }
    }
}
