using System;

namespace AspectCache.Core.Aspect
{
    /// <summary>
    /// Attribute for a parameter of a method that should not be considered when generating the key for the cache.
    /// Based on postsharp caching examples.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class NotCacheKeyAttribute : Attribute
    {
        public NotCacheKeyAttribute()
        {

        }
    }
}
