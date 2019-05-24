using System;

namespace AspectCache.Core.Aspect
{
    public sealed class InvalidateCacheAttribute : Attribute
    {
        public InvalidateCacheAttribute(string[] methodNames)
        {

        }

        public InvalidateCacheAttribute(Type declaringType, params string[] methodNames)
        {

        }
    }
}
