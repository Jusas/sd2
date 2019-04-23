using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SD2Tools.ReplayTools.Utils
{
    public static class PropertyUtils
    {

        public static void PopulateIntProperties(object obj)
        {
            var intProps = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(prop => prop.Name.EndsWith("Int", StringComparison.OrdinalIgnoreCase));

            foreach (var intProp in intProps)
            {
                var propName = intProp.Name.Substring(0, intProp.Name.Length - 3);
                PropertyInfo prop = null;
                if ((prop = obj.GetType().GetProperty(propName)) != null)
                {
                    if (prop.PropertyType == typeof(string))
                    {
                        if(Int32.TryParse((string)prop.GetValue(obj), NumberStyles.Any, CultureInfo.InvariantCulture, out var intVal))
                            intProp.SetValue(obj, intVal);
                    }
                }
            }
        }
    }
}
