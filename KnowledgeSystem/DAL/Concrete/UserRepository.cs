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
        private readonly DbSet<User> users;
        private readonly DbSet<Role> roles;

        /// <summary>
        /// Create new UserRepository instance
        /// </summary>
        /// <param name="knowledgeContext"></param>
        public UserRepository(KnowledgeSystemContext knowledgeContext)
        {
            context = knowledgeContext;
            users = context.Set<User>();
            roles = context.Set<Role>();
        }

        /// <summary>
        /// The method for creating new user entity in collection
        /// </summary>
        /// <param name="dalUser"></param>
        public void Create(DalUser dalUser)
        {
            var user = UserMapper.Map(dalUser);

            foreach (var role in dalUser.Roles)
            {
                var userRole = roles.FirstOrDefault(r => r.Name == role);
                user.Roles.Add(userRole);
            }

            users.Add(user);
        }

        /// <summary>
        /// The method for updating exsisting user in collection
        /// </summary>
        /// <param name="dalUser"></param>
        public void Update(DalUser dalUser)
        {
            var user = users.FirstOrDefault(u => u.Id == dalUser.Id);
            if (!ReferenceEquals(user, null))
            {
                user.Login = dalUser.Login;
                user.Email = dalUser.Email;
                user.Roles.Clear();
                foreach (var item in dalUser.Roles)
                {
                    var userRole = roles.FirstOrDefault(r => r.Name == item);
                    user.Roles.Add(userRole);
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
            users.Remove(users.FirstOrDefault(u => u.Id == id));
        }

        /// <summary>
        /// The method for getting user entity by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>DalUser</returns>
        public DalUser Get(int id)
        {
            return UserMapper.Map(users.FirstOrDefault(u => u.Id == id));
        }

        /// <summary>
        /// The method for getting all users
        /// </summary>
        /// <returns>DalUser collection</returns>
        public IEnumerable<DalUser> GetAll()
        {
            return UserMapper.Map(users.Include(u=>u.Roles).Select(u => u));            
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

            return UserMapper.Map(users.FirstOrDefault(func));
        }
    }
}
