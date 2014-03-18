using System;
using System.Linq.Expressions;

namespace Hopnscotch.Portal.Data.Helpers
{
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> GetByIdPredicate<T>(int id)
        {
            var itemParam = Expression.Parameter(typeof(T), "item");
            var itemPropertyExpr = Expression.Property(itemParam, "Id");
            var idParam = Expression.Constant(id);
            var newBody = Expression.MakeBinary(ExpressionType.Equal, itemPropertyExpr, idParam);
            var newLambda = Expression.Lambda(newBody, itemParam);
            return newLambda as Expression<Func<T, bool>>;
        }
    }
}