using AspectCache.Core.Components;
using CacheManager.Core;
using System;

namespace AspectCache.Core.Aspect
{
    public sealed class CacheAttribute : Attribute
    {
        private double _timeout;
        private ExpirationMode _expirationMode = ExpirationMode.Default;
        private const long TicksPerMinute = 600000000;

        /// <summary>
        /// Gets or sets the total time, in minutes, to retain the object in cache. Time is counted from the moment the result is cached.
        /// </summary>
        public double AbsoluteExpiration
        {
            set
            {
                ExpirationMode = ExpirationMode.Absolute;
                _timeout = value;
            }
        }

        /// <summary>
        /// Gets or sets the total time, in minutes, to ratain the object in cache. Time is counted from the last moment the cached method was accessed.
        /// </summary>
        public double SlidingExpiration
        {
            set
            {
                ExpirationMode = ExpirationMode.Sliding;
                _timeout = value;
            }
        }

        public bool IgnoreThisParameter { get; set; }

        public bool OnlyWhenAllParametersAreNull { get; set; }

        public TimeSpan Timeout
        {
            get
            {
                return new TimeSpan((long)(TicksPerMinute * _timeout));
            }
        }

        public Type CacheType { get; private set; }

        /// <summary>
        /// Either Absolute or Sliding.
        /// </summary>
        public ExpirationMode ExpirationMode
        {
            get
            {
                return _expirationMode;
            }
            private set
            {
                if (_expirationMode != ExpirationMode.Default)
                {
                    throw new ExpirationModeException("ExpriationMode has already been set and more than one type cannot be used together on a single cached method.");
                }
            }
        }

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
