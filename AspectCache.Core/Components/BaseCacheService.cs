using AspectCache.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AspectCache.Core.Components
{
    public class BaseCacheService : ICacheService
    {
        public T Get<T>(string key)
        {
            throw new NotImplementedException();
        }
    }
}
