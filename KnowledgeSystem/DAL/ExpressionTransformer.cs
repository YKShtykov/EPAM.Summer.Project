using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DAL
{
    /// <summary>
    /// Service class for expression tree transformation for GetByPredicate metods of Repositories
    /// </summary>
    /// <typeparam name="TFrom"></typeparam>
    /// <typeparam name="TTo"></typeparam>
    internal static class ExpressionTransformer<TFrom, TTo>
    {
        public class Visitor : ExpressionVisitor
        {
            private ParameterExpression parameter;

            public Visitor(ParameterExpression parameter)
            {
                this.parameter = parameter;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return parameter;
            }

            protected override Expression VisitMember(MemberExpression node)
            {
                var expr = Visit(node.Expression);
                MemberInfo newMember = expr.Type.GetMember(node.Member.Name).Single();

                return Expression.MakeMemberAccess(expr, newMember);
            }
        }

        public static Expression<Func<TTo, bool>> Tranform(Expression<Func<TFrom, bool>> expression)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TTo));
            Expression body = new Visitor(parameter).Visit(expression.Body);
            return Expression.Lambda<Func<TTo, bool>>(body, parameter);
        }
    }

    //internal static class ExpressionTransformer<TFrom, TTo> 
    //{
    //    public class Visitor : ExpressionVisitor
    //    {
    //        private ParameterExpression _parameter;

    //        public Visitor(ParameterExpression parameter)
    //        {
    //            _parameter = parameter;
    //        }

    //        protected override Expression VisitParameter(ParameterExpression node)
    //        {
    //            return _parameter;
    //        }

    //        protected override Expression VisitMember(MemberExpression node)
    //        {
    //            var expr = Visit(node.Expression);
    //            MemberInfo newMember = expr.Type.GetMember(node.Member.Name).Single();

    //            return Expression.MakeMemberAccess(expr, newMember);
    //        }
    //    }

    //    public static Expression<Func<TTo, bool>> Tranform(Expression<Func<TFrom, bool>> expression)
    //    {
    //        ParameterExpression parameter = Expression.Parameter(typeof(TTo));
    //        Expression body = new Visitor(parameter).Visit(expression.Body);
    //        return Expression.Lambda<Func<TTo, bool>>(body, parameter);
    //    }
    //}
}
