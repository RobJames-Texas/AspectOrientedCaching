using System;

namespace AspectCache.Core.Aspect
{
    public sealed class CacheAttribute : Attribute
    {
        public double AbsoluteExpiration { get; set; }

        public double SlidingExpiration { get; set; }

        public bool IgnoreThisParameter { get; set; }

        public bool OnlyWhenAllParametersAreNull { get; set; }

        public TimeSpan Timeout { get; private set; }

        public Type CacheType { get; private set; }

        public CacheAttribute()
        {
            // TODO: Setup a mechanism to get a default cache.
        }

        public CacheAttribute(Type cacheType)
        {
            CacheType = cacheType;
        }
    }
}
