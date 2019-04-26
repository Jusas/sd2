using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SD2API.Application.Search
{
    public class QueryBuilder : IQueryBuilder
    {
        public Expression<Func<TSourceType, bool>> NumericValueComparison<TSourceType>(string propertyName, NumericComparison comparison, double value)
        {
            var expTargetValue = Expression.Constant(value);
            var expParameter = Expression.Parameter(typeof(TSourceType), "x");
            Expression expGetProperty = Expression.PropertyOrField(expParameter, propertyName);

            Expression body = null;
            switch (comparison)
            {
                case NumericComparison.Eq:
                    body = Expression.Equal(expGetProperty, expTargetValue);
                    break;
                case NumericComparison.Gt:
                    body = Expression.GreaterThan(expGetProperty, expTargetValue);
                    break;
                case NumericComparison.Gte:
                    body = Expression.GreaterThanOrEqual(expGetProperty, expTargetValue);
                    break;
                case NumericComparison.Lt:
                    body = Expression.LessThan(expGetProperty, expTargetValue);
                    break;
                case NumericComparison.Lte:
                    body = Expression.LessThanOrEqual(expGetProperty, expTargetValue);
                    break;
            }

            var lambda = Expression.Lambda<Func<TSourceType, bool>>(body, expParameter);
            return lambda;
        }

        public Expression<Func<TSourceType, bool>> WordSearch<TSourceType>(string propertyName, StringComparison comparison, string searchString)
        {
            var expTargetValue = Expression.Constant(searchString);
            var expParameter = Expression.Parameter(typeof(TSourceType), "x");
            Expression expGetProperty = Expression.PropertyOrField(expParameter, propertyName);

            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var body = Expression.Call(expGetProperty, method, expTargetValue);
            var lambda = Expression.Lambda<Func<TSourceType, bool>>(body, expParameter);
            return lambda;
        }
    }
}
