using System;

namespace AspectCache.Core.Aspect
{
    public sealed class CacheAttribute : Attribute
    {
        public double AbsoluteExpiration { get; set; }

        public double SlidingExpiration { get; set; }

        public CacheAttribute()
        {

        }
    }
}
