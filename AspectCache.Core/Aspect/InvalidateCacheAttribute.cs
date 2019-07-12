using System;

namespace AspectCache.Core.Aspect
{
    /// <summary>
    /// Will invalidate the cache for the specified methods when the method with the attribute is called.
    /// Based on postsharp caching examples.
    /// </summary>
    public sealed class InvalidateCacheAttribute : Attribute
    {
        public string[] MethodNames { get; private set; }

        public InvalidateCacheAttribute(string[] methodNames)
        {
            MethodNames = methodNames;
        }

        public InvalidateCacheAttribute(Type declaringType, params string[] methodNames)
        {
            MethodNames = methodNames;
        }
    }
}
