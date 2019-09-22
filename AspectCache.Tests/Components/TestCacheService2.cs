using AspectCache.Core.Components;
using AspectCache.Core.Interfaces;
using CacheManager.Core;

namespace AspectCache.Tests.Components
{
    public class TestCacheService2 : BaseCacheService
    {
        public TestCacheService2(IKeyService keyService, ICacheManagerConfiguration cacheManagerConfiguration) : base(keyService, cacheManagerConfiguration)
        {

        }
    }
}
