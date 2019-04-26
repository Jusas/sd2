using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SD2API.Application.Search
{
    public interface IQueryBuilder
    {
        Expression<Func<TSourceType, bool>> NumericValueComparison<TSourceType>(string propertyName,
            NumericComparison comparison, double value);

        Expression<Func<TSourceType, bool>> WordSearch<TSourceType>(string propertyName, StringComparison stringComparison, string searchString);
    }
}
