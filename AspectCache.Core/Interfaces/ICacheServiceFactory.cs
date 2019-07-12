using AspectCache.Core.Components;
using System;
using System.Collections.Generic;

namespace AspectCache.Core.Interfaces
{
    public interface ICacheServiceFactory
    {
        ICacheService GetCache<T>() where T : BaseCacheService;

        ICacheService GetCache(Type cacheType);

        IEnumerable<ICacheService> All();
    }
}