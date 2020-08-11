using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;

namespace ZideunMail.Std.Tools
{
    public static class Extensions
    {
        public static void AddRange<T>(this ICollection<T> initial, IEnumerable<T> other)
        {
            if (other == null)
                return;

            if (initial is List<T> list)
            {
                list.AddRange(other);
                return;
            }

            foreach (var local in other)
            {
                initial.Add(local);
            }
        }

        public static bool IsNullOrEmpty<T>(this ICollection<T> source)
        {
            return source == null || source.Count == 0;
        }

        public static void AddRange(this NameValueCollection initial, NameValueCollection other)
        {
            if (initial == null)
                throw new ArgumentNullException(nameof(initial));

            if (other == null)
                return;

            foreach (var item in other.AllKeys)
            {
                initial.Add(item, other[item]);
            }
        }

        [DebuggerStepThrough]
        public static bool HasValue(this string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
    }
}
