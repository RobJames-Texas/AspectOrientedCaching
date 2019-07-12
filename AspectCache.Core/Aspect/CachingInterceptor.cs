using AspectCache.Core.Extensions;
using AspectCache.Core.Interfaces;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspectCache.Core.Aspect
{
    // Based on the caching interceptor by Mark Rogers (mb4wav on GitHub)
    public class CachingInterceptor : IInterceptor
    {
        private ICacheServiceFactory _cacheServiceFactory;

        public CachingInterceptor(ICacheServiceFactory cacheServiceFactory)
        {
            _cacheServiceFactory = cacheServiceFactory ?? throw new ArgumentNullException(nameof(cacheServiceFactory));
        }

        public void Intercept(IInvocation invocation)
        {
            bool hasCacheAttribute = DoesInvocationHaveTheCacheAttribute(invocation);
            bool hasInvalidateCacheAttribute = DoesInvocationHaveTheInvalidateCacheAttribute(invocation);

            if (hasCacheAttribute)
            {
                // This method should be cached.
                CacheAttribute cacheAttribute = (CacheAttribute)invocation.MethodInvocationTarget.GetCustomAttributes(typeof(CacheAttribute), false).FirstOrDefault();

                ICacheService cacheService =  _cacheServiceFactory.GetCache(cacheAttribute.CacheType);

                if (cacheAttribute != null)
                {
                    cacheService.GetByInvocation(invocation, cacheAttribute);
                }
            }
            else if (hasInvalidateCacheAttribute)
            {
                // This method invalidates a cached object.
                InvalidateCacheAttribute invalidateCacheAttribute = (InvalidateCacheAttribute)invocation.MethodInvocationTarget.GetCustomAttributes(typeof(InvalidateCacheAttribute), false).FirstOrDefault();
                IEnumerable<ICacheService> cacheServices = _cacheServiceFactory.All();

                // Invalidate the cache on all cache services.
                cacheServices.ToList().ForEach(x => x.DeleteByInvocation(invocation, invalidateCacheAttribute.MethodNames));
            }
            else
            {
                // Niether cache attribute is on the method. Let it process normally.
                invocation.Proceed();
            }
            return;
        }

        private static bool DoesInvocationHaveTheCacheAttribute(IInvocation invocation)
        {
            var methodInvocationTarget = invocation.MethodInvocationTarget;

            return methodInvocationTarget.HasCacheAttribute();
        }

        private static bool DoesInvocationHaveTheInvalidateCacheAttribute(IInvocation invocation)
        {
            var methodInvocationTarget = invocation.MethodInvocationTarget;

            return methodInvocationTarget.HasInvalidateCacheAttribute();
        }
    }
}
