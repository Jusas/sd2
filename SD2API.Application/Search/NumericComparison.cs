using System;
using System.Collections.Generic;
using System.Text;

namespace SD2API.Application.Search
{
    public enum NumericComparison
    {
        Lt,
        Lte,
        Eq,
        Gt,
        Gte,
        Undefined
    }

    public static class NumericComparisonOperator
    {
        public static NumericComparison ToNumericComparison(this string str)
        {
            return Enum.TryParse(typeof(NumericComparison), str, true, out var val) 
                ? (NumericComparison)val : NumericComparison.Undefined;
        }
    }
}
