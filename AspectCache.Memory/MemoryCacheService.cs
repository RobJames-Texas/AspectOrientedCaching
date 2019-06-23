using AspectCache.Core.Components;
using CacheManager.Core;
using Microsoft.Extensions.Configuration;
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;

namespace AspectCache.Memory
{
    public class MemoryCacheService : BaseCacheService
    {
        public MemoryCacheService() : base(new DefaultCacheKeyService(), GetConfiguration())
        {
        }

        private static ICacheManagerConfiguration GetConfiguration()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddJsonFile("memorycache.json");
            return configurationBuilder.Build().GetCacheConfiguration("memory");
        }
    }
}
