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
    /// User repository class implements Repository pattern for user collection
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly KnowledgeSystemContext context;

        /// <summary>
        /// Create new UserRepository instance
        /// </summary>
        /// <param name="knowledgeContext"></param>
        public UserRepository(KnowledgeSystemContext knowledgeContext)
        {
            context = knowledgeContext;
        }

        /// <summary>
        /// The method for creating new user entity in collection
        /// </summary>
        /// <param name="entity"></param>
        public void Create(DalUser entity)
        {
            var user = UserMapper.Map(entity);

            foreach (var role in entity.Roles)
            {
                var userRole = context.Set<Role>().FirstOrDefault(r => r.Name == role);
                user.Roles.Add(userRole);
            }

            context.Set<User>().Add(user);
        }

        /// <summary>
        /// The method for updating exsisting user in collection
        /// </summary>
        /// <param name="entity"></param>
        public void Update(DalUser entity)
        {
            var user = context.Set<User>().FirstOrDefault(u => u.Id == entity.Id);
            if (!ReferenceEquals(user, null))
            {
                user.Login = entity.Login;
                user.Email = entity.Email;
                user.Roles.Clear();
                foreach (var item in entity.Roles)
                {
                    user.Roles.Add(context.Set<Role>().FirstOrDefault(r => r.Name == item));
                }
                context.Entry(user).State = EntityState.Modified;
            }
        }

        /// <summary>
        /// The method for deleting user from collection
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            context.Set<User>().Remove(context.Set<User>().FirstOrDefault(u => u.Id == id));
        }

        /// <summary>
        /// The method for getting user entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DalUser</returns>
        public DalUser Get(int id)
        {
            return UserMapper.Map(context.Users.FirstOrDefault(u => u.Id == id));
        }

        /// <summary>
        /// The method for getting all users
        /// </summary>
        /// <returns>DalUser collection</returns>
        public IEnumerable<DalUser> GetAll()
        {
            return UserMapper.Map(context.Set<User>().Include(u=>u.Roles).Select(u => u));            
        }

        /// <summary>
        /// The method for getting user entity by predicate
        /// </summary>
        /// <param name="f"></param>
        /// <returns>DalUser</returns>
        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            var expr = ExpressionTransformer<DalUser, User>.Tranform(f);
            var func = expr.Compile();

            return UserMapper.Map(context.Set<User>().FirstOrDefault(func));
        }
    }
}
