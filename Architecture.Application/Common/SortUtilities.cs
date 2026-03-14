using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace Architecture.Application.Common
{
    public static class SortUtilities
    {
        private static class SortCache<T>
        {
            internal static readonly Dictionary<string, Expression<Func<T, object>>> map =
                BuildInternal<T>(exclude_properties: null);
        }

        public static Dictionary<string, Expression<Func<T, object>>> BuildSortMap<T>(params string[]? exclude_properties)
        {
            if (exclude_properties is null || exclude_properties.Length == 0)
                return SortCache<T>.map;

            return BuildInternal<T>(exclude_properties);
        }

        private static Dictionary<string, Expression<Func<T, object>>> BuildInternal<T>(string[]? exclude_properties)
        {
            var excluded = new HashSet<string>(exclude_properties ?? Array.Empty<string>(), StringComparer.OrdinalIgnoreCase);

            var props = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(p => p.CanRead)
                .Where(p => !excluded.Contains(p.Name))
                .Where(p => IsSortableType(p.PropertyType));

            var result = new Dictionary<string, Expression<Func<T, object>>>(StringComparer.OrdinalIgnoreCase);

            foreach (var p in props)
            {
                var param = Expression.Parameter(typeof(T), "x");
                var member = Expression.Property(param, p);
                var boxed = Expression.Convert(member, typeof(object));
                var lambda = Expression.Lambda<Func<T, object>>(boxed, param);

                result[p.Name] = lambda;
            }

            return result;
        }

        private static bool IsSortableType(Type t)
        {
            t = Nullable.GetUnderlyingType(t) ?? t;

            return t.IsEnum
                   || t.IsPrimitive
                   || t == typeof(string)
                   || t == typeof(decimal)
                   || t == typeof(DateTime)
                   || t == typeof(Guid);
        }
    }
}
