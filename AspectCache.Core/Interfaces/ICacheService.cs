namespace AspectCache.Core.Interfaces
{
    public interface ICacheService
    {
        T Get<T>(string key);

        bool Add<T>(string key, T value);

        bool Remove(string key);

        void Clear();
    }
}
