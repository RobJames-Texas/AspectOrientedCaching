﻿using AspectCache.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspectCache.Core.Components
{
    /// <summary>
    /// Factory to return the appropriate cache service based upon the type requested.
    /// Cache services should be registered by the dependency injection.
    /// </summary>
    public class CacheServiceFactory : ICacheServiceFactory
    {
        private readonly IEnumerable<ICacheService> _cacheServices;

        public CacheServiceFactory(IEnumerable<ICacheService> cacheServices)
        {
            _cacheServices = cacheServices ?? throw new ArgumentNullException(nameof(cacheServices));
        }

        public ICacheService GetCache<T>() where T : BaseCacheService
        {
            return _cacheServices.FirstOrDefault(x => x is T) ?? throw new UnregisteredCacheServiceException(typeof(T).ToString());
        }

        public ICacheService GetCache(Type cacheType)
        {
            if (!cacheType.GetInterfaces().Contains(typeof(ICacheService)))
            {
                throw new ArgumentException(nameof(cacheType) + " parameter must be a type that implements ICacheService");
            }
            return _cacheServices.FirstOrDefault(x => x.GetType() == cacheType) ?? throw new UnregisteredCacheServiceException(cacheType.ToString());
        }

        public IEnumerable<ICacheService> All()
        {
            return _cacheServices;
        }
    }
}
