using AspectCache.Core.Components;

namespace AspectCache.Core.Interfaces
{
    public interface ICacheServiceFactory
    {
        ICacheService GetCache<T>() where T : BaseCacheService;
    }
}