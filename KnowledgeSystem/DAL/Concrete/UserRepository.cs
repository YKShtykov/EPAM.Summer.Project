using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface;
using DAL.Mappers;
using ORM;
using System.Data.Entity;
using System.Reflection;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly KnowledgeSystemContext context;

        public UserRepository(KnowledgeSystemContext knowledgeContext)
        {
            context = knowledgeContext;
        }

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

        public void Delete(int id)
        {
            context.Set<User>().Remove(context.Set<User>().FirstOrDefault(u => u.Id == id));
        }

        public DalUser Get(int id)
        {
            return UserMapper.Map(context.Users.FirstOrDefault(u => u.Id == id));
        }

        public IEnumerable<DalUser> GetAll()
        {
            return UserMapper.Map(context.Set<User>().Include(u=>u.Roles).Select(u => u));            
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            var expr = ExpressionTransformer<DalUser, User>.Tranform(f);
            var func = expr.Compile();

            return UserMapper.Map(context.Set<User>().FirstOrDefault(func));
        }
    }
}
