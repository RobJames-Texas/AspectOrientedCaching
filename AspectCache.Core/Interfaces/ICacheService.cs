namespace AspectCache.Core.Interfaces
{
    public interface ICacheService
    {
        T Get<T>(string key);
    }
}
