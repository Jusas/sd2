using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SD2API.Application.Infrastructure.Expressions
{
    internal static class ExpressionExtensions
    {
        /// <summary>
        /// Creates a lambda expression that represents a conditional OR operation
        /// </summary>
        /// <param name="left">An expression to set the left property of the binary expression</param>
        /// <param name="right">An expression to set the right property of the binary expression</param>
        /// <returns>A binary expression that has the node type property equal to OrElse, 
        /// and the left and right properties set to the specified values</returns>
        public static Expression<Func<T, Boolean>> OrElse<T>(this Expression<Func<T, Boolean>> left, Expression<Func<T, Boolean>> right)
        {
            Expression<Func<T, Boolean>> combined = Expression.Lambda<Func<T, Boolean>>(
                Expression.OrElse(
                    left.Body,
                    new ExpressionParameterReplacer(right.Parameters, left.Parameters).Visit(right.Body)
                ), left.Parameters);

            return combined;
        }

        /// <summary>
        /// Creates a lambda expression that represents a conditional AND operation
        /// </summary>
        /// <param name="left">An expression to set the left property of the binary expression</param>
        /// <param name="right">An expression to set the right property of the binary expression</param>
        /// <returns>A binary expression that has the node type property equal to AndAlso, 
        /// and the left and right properties set to the specified values</returns>
        public static Expression<Func<T, Boolean>> AndAlso<T>(this Expression<Func<T, Boolean>> left, Expression<Func<T, Boolean>> right)
        {
            Expression<Func<T, Boolean>> combined = Expression.Lambda<Func<T, Boolean>>(
                Expression.AndAlso(
                    left.Body,
                    new ExpressionParameterReplacer(right.Parameters, left.Parameters).Visit(right.Body)
                ), left.Parameters);

            return combined;
        }
    }
}
