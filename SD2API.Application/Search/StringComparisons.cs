using System;
using System.Collections.Generic;
using System.Text;

namespace SD2API.Application.Search
{
    public enum StringComparison
    {
        Contains,
        Undefined
    }

    public static class StringComparisonOperator
    {
        public static StringComparison ToStringComparison(this string str)
        {
            return Enum.TryParse(typeof(StringComparison), str, true, out var val)
                ? (StringComparison)val : StringComparison.Undefined;
        }
    }

}
