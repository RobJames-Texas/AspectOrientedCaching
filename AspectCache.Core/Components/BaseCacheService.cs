using AspectCache.Core.Aspect;
using AspectCache.Core.Interfaces;
using CacheManager.Core;
using Castle.DynamicProxy;
using System;

namespace AspectCache.Core.Components
{
    // Heavily borrowed from Mark Rogers(m4bwav) caching project.
    public abstract class BaseCacheService : ICacheService
    {
        // Variable to store the cachemanager instance. To be set by classes that inherit from this one.
        protected readonly ICacheManager<object> _cacheManager;

        protected readonly IKeyService _keyService;


        public BaseCacheService(IKeyService keyService, ICacheManagerConfiguration cacheManagerConfiguration)
        {
            _keyService = keyService ?? throw new ArgumentNullException(nameof(keyService));
            if (cacheManagerConfiguration == null)
            {
                throw new ArgumentNullException(nameof(cacheManagerConfiguration));
            }

            _cacheManager = CacheFactory.FromConfiguration<object>(cacheManagerConfiguration);
        }

        /// <summary>
        /// Get the object from the cache for the given key
        /// </summary>
        public T Get<T>(string key)
        {
            return CastResultToTypeOrDefault<T>(_cacheManager.Get(key));
        }

        /// <summary>
        /// Add an object to the cache for the given key. Fails if key already exists.
        /// </summary>
        public bool Add<T>(string key, T value)
        {
            return _cacheManager.Add(key, value);
        }

        /// <summary>
        /// Remove the cached object for the given key.
        /// </summary>
        public bool Remove(string key)
        {
            return _cacheManager.Remove(key);
        }

        /// <summary>
        /// Clears this cache.
        /// </summary>
        public void Clear()
        {
            _cacheManager.Clear();
        }

        public void GetByInvocation(IInvocation invocation, CacheAttribute cacheAttribute)
        {
            var key = _keyService.GenerateUniqueKeyForCall(invocation);

            var result = _cacheManager.Get(key);

            if (result != null)
            {
                invocation.ReturnValue = result;
                return;
            }

            invocation.Proceed();
            var value = invocation.ReturnValue;

            _cacheManager.Add(new CacheItem<object>(key, value, cacheAttribute.ExpirationMode, cacheAttribute.Timeout));
        }

        public void DeleteByInvocation(IInvocation invocation, string[] methodNames)
        {
            // Loop over passed in method names and remove their keys from the cache.
            foreach (string methodName in methodNames)
            {
                var key = _keyService.GenerateUniqueKeyForCall(invocation, methodName);
                _cacheManager.Remove(key);
            }
            invocation.Proceed();
        }

        private static T CastResultToTypeOrDefault<T>(object result)
        {
            if (!(result is T))
            {
                return default(T);
            }
            else
            {
                return (T)result;
            }
        }
    }
}
