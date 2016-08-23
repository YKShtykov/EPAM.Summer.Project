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
            context.Set<Profile>().Add(new Profile() { Id = user.Id, BirthDate = default(DateTime).ToString() });
            context.SaveChanges();
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
                context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var user = context.Set<User>().Include(u => u.Profile).FirstOrDefault(u => u.Id == id);

            if (ReferenceEquals(user,null))
            {
                context.Set<Profile>().Remove(user.Profile);
                context.Set<User>().Remove(user);
                context.SaveChanges(); 
            }
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
            return UserMapper.Map(context.Set<User>().FirstOrDefault(ExpressionTransformer<DalUser, User>.Tranform(f).Compile()));
        }

        //public void Create(DalUser e)
        //{
        //    var user = UserMapper.Map(e);

        //    foreach (var role in e.Roles)
        //    {
        //        var userRole = context.Set<Role>().FirstOrDefault(r => r.Name == role);
        //        user.Roles.Add(userRole);
        //    }

        //    context.Set<User>().Add(user);
        //    context.Set<Profile>().Add(new Profile() { Id = user.Id, BirthDate = default(DateTime).ToString() });
        //    context.SaveChanges();
        //}

        //public void Delete(int id)
        //{
        //    var user = context.Set<User>().Include(u=>u.Profile).FirstOrDefault(u => u.Id == id);
        //    context.Set<Profile>().Remove(user.Profile);
        //    context.Set<User>().Remove(user);
        //    context.SaveChanges();
        //}

        //public IEnumerable<DalUser> GetAll()
        //{
        //    var users = context.Users.Select(u => u);
        //    var dalUsers = new List<DalUser>();
        //    foreach (var item in users)
        //    {
        //        dalUsers.Add(UserMapper.Map(item));
        //    }
        //    return dalUsers;
        //}

        //public DalUser GetById(int key)
        //{
        //    return UserMapper.Map(context.Users.FirstOrDefault(u => u.Id == key));
        //}

        //public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Update(DalUser entity)
        //{
        //    var user = context.Set<User>().FirstOrDefault(u => u.Id == entity.Id);
        //    if (!ReferenceEquals(user, null))
        //    {
        //        user.Roles.Clear();
        //        foreach (var item in entity.Roles)
        //        {
        //            user.Roles.Add(context.Set<Role>().FirstOrDefault(r => r.Name == item));
        //        }
        //        context.Entry(user).State = EntityState.Modified;
        //        context.SaveChanges();
        //    }

        //}

        //public bool ConsistEmail(string key)
        //{
        //    return !ReferenceEquals(context.Set<User>().FirstOrDefault(user => user.Email == key), null);
        //}

        //public bool ConsistLogin(string key)
        //{
        //    return !ReferenceEquals(context.Set<User>().FirstOrDefault(user => user.Login == key), null);
        //}

        public DalUser LoginUser(string emailOrLogin)
        {
            User user = context.Set<User>().FirstOrDefault(u => u.Email == emailOrLogin);

            if (user == null) user = context.Set<User>().FirstOrDefault(u => u.Login == emailOrLogin);
            if (user == null) throw new Exception("check your data");

            return UserMapper.Map(user);
        }

        //public int GetSkillLevel(int userId, int skillId)
        //{
        //    return context.Set<UserSkill>().First(us => us.UserId == userId && us.SkillId == skillId).Level;
        //}

        //public Dictionary<DalSkill, int> GetAllSkillLevels(int userId)
        //{
        //    var skills = new Dictionary<DalSkill, int>();
        //    var categories = context.Set<Category>().Select(c => c).Include(c => c.Skills);
        //    foreach (var item in categories)
        //    {
        //        foreach (var skill in item.Skills)
        //        {
        //            var entity = context.Set<UserSkill>().FirstOrDefault(us => us.User.Id == userId && us.Skill.Id == skill.Id);
        //            int level = (!ReferenceEquals(entity, null)) ? entity.Level : 0;
        //            skills.Add(SkillMapper.Map(skill), level);
        //        }
        //    }

        //    return skills;
        //}

        //public void UpdateSkillLevel(int userId, int skillId, int level)
        //{
        //    var userSkill = context.Set<UserSkill>().FirstOrDefault(us => us.User.Id == userId && us.Skill.Id == skillId);
        //    if (!ReferenceEquals(userSkill, null))
        //    {
        //        userSkill.Level = level;
        //        context.Entry(userSkill).State = EntityState.Modified;
        //    }
        //    else
        //    {
        //        userSkill = new UserSkill()
        //        {
        //            User = context.Set<User>().FirstOrDefault(u => u.Id == userId),
        //            Skill = context.Set<Skill>().FirstOrDefault(s => s.Id == skillId),
        //            Level = level
        //        };
        //        context.Set<UserSkill>().Add(userSkill);
        //    }            
        //    context.SaveChanges();
        //}

        //public void UpdateAllSkillLevels(int userId, IDictionary<int, int> skillLevel)
        //{
        //    foreach (var item in skillLevel)
        //    {
        //        UpdateSkillLevel(userId, item.Key, item.Value);
        //    };
        //}

        internal static class ExpressionTransformer<DalUser, User> /*where TTo : TFrom*/
        {
            public class Visitor : ExpressionVisitor
            {
                private ParameterExpression _parameter;

                public Visitor(ParameterExpression parameter)
                {
                    _parameter = parameter;
                }

                protected override Expression VisitParameter(ParameterExpression node)
                {
                    return _parameter;
                }
            }

            public static Expression<Func<User, bool>> Tranform(Expression<Func<DalUser, bool>> expression)
            {
                ParameterExpression parameter = Expression.Parameter(typeof(User));
                Expression body = new Visitor(parameter).Visit(expression.Body);
                return Expression.Lambda<Func<User, bool>>(body, parameter);
            }
        }
    }
}
