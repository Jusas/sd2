using System;
using System.Collections.Generic;
using System.Text;

namespace SD2API.Application.Search
{
    [AttributeUsage(validOn: AttributeTargets.Property, AllowMultiple = false)]
    public class SearchablePropertyAttribute : Attribute
    {
        public string PropertyName { get; set; }

        public SearchablePropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        public SearchablePropertyAttribute()
        {
        }
    }
}
