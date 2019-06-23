using AspectCache.Core.Components;
using AspectCache.Core.Interfaces;
using CacheManager.Core;

namespace AspectCache.Tests.Components
{
    public class TestCacheService1 : BaseCacheService
    {
        public TestCacheService1(IKeyService keyService, ICacheManagerConfiguration cacheManagerConfiguration) : base(keyService, cacheManagerConfiguration)
        {

        }
    }
}
