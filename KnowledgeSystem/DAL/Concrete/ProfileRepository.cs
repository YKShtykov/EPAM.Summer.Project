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

        public void Create(int id)
        {
            context.Set<Profile>().Add(new Profile() { Id = id, BirthDate = default(DateTime).ToString() });
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
                ormProfile.Image = profile.Image;
                ormProfile.ImageMimeType = profile.ImageMimeType;
                context.Entry(ormProfile).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            context.Set<Profile>().Remove(context.Set<Profile>().FirstOrDefault(p => p.Id == id));
        }

        public DalProfile Get(int key)
        {
            return ProfileMapper.Map(context.Set<Profile>().FirstOrDefault(u => u.Id == key));
        }

        public IEnumerable<DalProfile> GetAll()
        {
            return ProfileMapper.Map(context.Set<Profile>().Select(p=>p));
        }

        public DalProfile GetByPredicate(Expression<Func<DalProfile, bool>> f)
        {
            var expr = ExpressionTransformer<DalProfile, Profile>.Tranform(f);
            var func = expr.Compile();

            return ProfileMapper.Map(context.Set<Profile>().FirstOrDefault(func));
        }        
    }
}
