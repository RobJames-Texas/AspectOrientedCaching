using Castle.DynamicProxy;

namespace AspectCache.Core.Interfaces
{
    public interface IKeyService
    {
        string GenerateUniqueKeyForCall(IInvocation invocation, string method = null);
    }
}
