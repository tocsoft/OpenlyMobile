using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenlyLocal.Core.Extensions
{
    public static class StringExtension
    {
        public static bool ContainsAny(this string str, params string[] parts)
        {
            return parts.Any(x => str.Contains(x));
        }
        public static bool ContainsAll(this string str, params string[] parts)
        {
            return parts.Where(x=>!string.IsNullOrWhiteSpace(x)).All(x => str.Contains(x));
        }
    }
}
