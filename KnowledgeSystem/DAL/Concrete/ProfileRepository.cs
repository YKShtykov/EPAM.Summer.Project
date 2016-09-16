using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface;
using DAL.Mappers;
using ORM;
using System.Data.Entity;

namespace DAL
{
    /// <summary>
    /// Profile repository class implements Repository pattern for profile collection
    /// </summary>
    public class ProfileRepository : IProfileRepository
    {
        private readonly KnowledgeSystemContext context;
        private readonly DbSet<Profile> profiles;

        /// <summary>
        /// Create ProfileRepository instance
        /// </summary>
        /// <param name="knowledgeContext"></param>
        public ProfileRepository(KnowledgeSystemContext knowledgeContext)
        {
            context = knowledgeContext;
            profiles = context.Set<Profile>();
        }

        /// <summary>
        /// The method for creating new profile entity in collection
        /// </summary>
        /// <param name="profile"></param>
        public void Create(DalProfile profile)
        {
            profiles.Add(ProfileMapper.Map(profile));
        }

        /// <summary>
        /// The method for updating exsisting profile in collection
        /// </summary>
        /// <param name="profile"></param>
        public void Update(DalProfile profile)
        {
            var ormProfile = profiles.FirstOrDefault(p => p.Id == profile.Id);
            if (ormProfile != null)
            {
                ormProfile.LastName = profile.LastName;
                ormProfile.FirstName = profile.FirstName;
                ormProfile.MiddleName = profile.MiddleName;
                ormProfile.ContactEmail = profile.ContactEmail;
                ormProfile.ContactPhone = profile.ContactPhone;
                ormProfile.AdditionalInfo = profile.AdditionalInfo;
                ormProfile.BirthDate = profile.BirthDate.ToString();
                ormProfile.City = profile.City;
                ormProfile.Gender = profile.Gender;
                ormProfile.RelationshipStatus = profile.RelationshipStatus;
                if (!ReferenceEquals(profile.Image,null))
                {
                    ormProfile.Image = profile.Image;
                    ormProfile.ImageMimeType = profile.ImageMimeType;
                }                
                context.Entry(ormProfile).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// The method for deleting profile entity from collection
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            profiles.Remove(profiles.FirstOrDefault(p => p.Id == id));
        }

        /// <summary>
        /// The method for getting profile entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DalProfile</returns>
        public DalProfile Get(int id)
        {            
            return ProfileMapper.Map(profiles.FirstOrDefault(u => u.Id == id));
        }

        /// <summary>
        /// The method for getting all profiles
        /// </summary>
        /// <returns>DalProfile collection</returns>
        public IEnumerable<DalProfile> GetAll()
        {
            return ProfileMapper.Map(profiles.Select(p=>p));
        }

        /// <summary>
        /// The method for getting profile entity by predicate
        /// </summary>
        /// <param name="f"></param>
        /// <returns>DalProfile</returns>
        public DalProfile GetByPredicate(Expression<Func<DalProfile, bool>> f)
        {
            var expr = ExpressionTransformer<DalProfile, Profile>.Tranform(f);
            var func = expr.Compile();

            return ProfileMapper.Map(profiles.FirstOrDefault(func));
        }
    }
}
