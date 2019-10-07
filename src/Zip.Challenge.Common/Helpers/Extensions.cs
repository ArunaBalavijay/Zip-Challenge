using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Zip.Challenge.Common.Helpers
{
    public static class Extensions
    {
        public static TTarget ConvertTo<TSource, TTarget>(this TSource source) where TTarget : new()
        {
            if (source == null) return default;

            var target = new TTarget();
            var sourceProps = typeof(TSource).GetRuntimeProperties().ToDictionary(prop => prop.Name);
            var targetProps = typeof(TTarget).GetRuntimeProperties();

            foreach (var prop in targetProps)
            {
                if (prop.CanWrite && sourceProps.ContainsKey(prop.Name))
                {
                    prop.SetValue(target, sourceProps[prop.Name].GetValue(source));
                }
            }

            return target;
        }

        public static List<TTarget> ConvertListTo<TSource, TTarget>(this List<TSource> source) where TTarget : new()
        {
            return source.Select(item => item.ConvertTo<TSource, TTarget>()).ToList();
        }
    }
}
