using AspectCache.Core.Components;
using System.Collections.Generic;

namespace AspectCache.Core.Interfaces
{
    public interface ICacheServiceFactory
    {
        ICacheService GetCache<T>() where T : BaseCacheService;

        IEnumerable<ICacheService> All();
    }
}