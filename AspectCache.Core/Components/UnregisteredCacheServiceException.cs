using System;
using System.Runtime.Serialization;

namespace AspectCache.Core.Components
{
    [Serializable]
    public class UnregisteredCacheServiceException : Exception
    {
        public UnregisteredCacheServiceException() { }

        protected UnregisteredCacheServiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public UnregisteredCacheServiceException(string name)
            : base($"Unregistered CacheService requested: {name}") { }

    }
}
