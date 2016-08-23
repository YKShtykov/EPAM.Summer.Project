using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface;
using DAL.Mappers;
using ORM;
using System.Data.Entity;

namespace DAL
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly KnowledgeSystemContext context;

        public ProfileRepository(KnowledgeSystemContext knowledgeContext)
        {
            context = knowledgeContext;
        }

        public void Create(DalProfile e)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DalProfile> GetAll()
        {
            throw new NotImplementedException();
        }

        public DalProfile Get(int key)
        {
            return ProfileMapper.MapProfile(context.Set<Profile>().FirstOrDefault(u => u.Id == key));
        }

        public DalProfile GetByPredicate(Expression<Func<DalProfile, bool>> f)
        {
            throw new NotImplementedException();
        }

        public void Update(DalProfile profile)
        {
            var ormProfile = context.Set<Profile>().FirstOrDefault(p => p.Id == profile.Id);
            if (ormProfile != null)
            {
                ormProfile.LastName = profile.LastName;
                ormProfile.FirstName = profile.FirstName;
                ormProfile.MiddleName = profile.MiddleName;
                ormProfile.AdditionalInfo = profile.AdditionalInfo;
                ormProfile.BirthDate = profile.BirthDate.ToString();
                ormProfile.City = profile.City;
                ormProfile.Gender = profile.Gender;
                ormProfile.RelationshipStatus = profile.RelationshipStatus;

                context.Entry(ormProfile).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
