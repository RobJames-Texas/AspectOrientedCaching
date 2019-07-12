using Castle.DynamicProxy;
using System;

namespace AspectCache.Core.Interfaces
{
    public interface ICacheService
    {
        T Get<T>(string key);

        bool Add<T>(string key, T value);

        bool Remove(string key);

        void Clear();

        void GetByInvocation(IInvocation invocation, TimeSpan? duration = null);

        void DeleteByInvocation(IInvocation invocation);
    }
}
