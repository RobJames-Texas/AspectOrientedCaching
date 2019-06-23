using System;

namespace AspectCache.Core.Components
{
    [Serializable]
    public class UnregisteredCacheServiceException : Exception
    {
        public UnregisteredCacheServiceException() { }

        public UnregisteredCacheServiceException(string name)
            : base($"Unregistered CacheService requested: {name}") { }

    }
}
