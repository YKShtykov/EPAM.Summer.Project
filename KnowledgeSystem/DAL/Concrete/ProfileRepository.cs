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

        /// <summary>
        /// Create ProfileRepository instance
        /// </summary>
        /// <param name="knowledgeContext"></param>
        public ProfileRepository(KnowledgeSystemContext knowledgeContext)
        {
            context = knowledgeContext;
        }

        /// <summary>
        /// The method for creating new profile entity in collection
        /// </summary>
        /// <param name="id"></param>
        public void Create(int id)
        {
            context.Set<Profile>().Add(new Profile() { Id = id, BirthDate = default(DateTime).ToString() });
        }

        /// <summary>
        /// The method for updating exsisting profile in collection
        /// </summary>
        /// <param name="profile"></param>
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

        /// <summary>
        /// The method for deleting profile entity from collection
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            context.Set<Profile>().Remove(context.Set<Profile>().FirstOrDefault(p => p.Id == id));
        }

        /// <summary>
        /// The method for getting profile entity by Id
        /// </summary>
        /// <param name="key"></param>
        /// <returns>DalProfile</returns>
        public DalProfile Get(int key)
        {
            return ProfileMapper.Map(context.Set<Profile>().FirstOrDefault(u => u.Id == key));
        }

        /// <summary>
        /// The method for getting all profiles
        /// </summary>
        /// <returns>DalProfile collection</returns>
        public IEnumerable<DalProfile> GetAll()
        {
            return ProfileMapper.Map(context.Set<Profile>().Select(p=>p));
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

            return ProfileMapper.Map(context.Set<Profile>().FirstOrDefault(func));
        }

        /// <summary>
        /// The method for creating new profile entity in collection
        /// </summary>
        /// <param name="entity"></param>
        public void Create(DalProfile entity)
        {
            throw new NotImplementedException();
        }
    }
}
