using AspectCache.Core.Aspect;
using System;
using System.Reflection;

namespace AspectCache.Core.Extensions
{
    /// <summary>
    /// Extension methods used to determine if a method should be intercepted for caching.
    /// Based on Mark Rogers work (m4bwav)
    /// </summary>
    public static class ReflectionExtensions
    {
        public static bool HasCacheAttribute(this MethodInfo method)
        {
            return Attribute.IsDefined(method, typeof(CacheAttribute));
        }

        public static bool HasInvalidateCacheAttribute(this MethodInfo method)
        {
            return Attribute.IsDefined(method, typeof(InvalidateCacheAttribute));
        }
    }
}
