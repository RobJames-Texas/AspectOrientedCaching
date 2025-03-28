﻿using AspectCache.Core.Components;
using AspectCache.Core.Interfaces;
using AspectCache.Tests.Components;
using CacheManager.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;

namespace AspectCache.Tests
{
    [TestClass]
    public class CacheServiceFactoryTests
    {
        [TestMethod]
        public void Constructor_ShouldThrowExceptionForNullServiceList()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => new CacheServiceFactory(null));
        }

        [TestMethod]
        public void GetCache_ShouldReturnCacheOfAppropriateType()
        {
            Mock<IKeyService> mockKeyService = new Mock<IKeyService>();

            List<BaseCacheService> cacheServices = new List<BaseCacheService>
            {
                new TestCacheService1(mockKeyService.Object, GetConfiguration("TestCacheService1")),
                new TestCacheService2(mockKeyService.Object, GetConfiguration("TestCacheService2"))
            };

            CacheServiceFactory factory = new CacheServiceFactory(cacheServices);

            var cache1 = factory.GetCache<TestCacheService1>();
            var cache2 = factory.GetCache<TestCacheService2>();

            Assert.IsNotNull(cache1);
            Assert.IsNotNull(cache2);

            Assert.IsInstanceOfType(cache1, typeof(TestCacheService1));
            Assert.IsInstanceOfType(cache2, typeof(TestCacheService2));
        }

        [TestMethod]
        public void GetCache_ShouldThrowExceptionWhenUnregisteredTypeRequested()
        {
            Mock<IKeyService> mockKeyService = new Mock<IKeyService>();

            List<BaseCacheService> cacheServices = new List<BaseCacheService>
            {
                new TestCacheService1(mockKeyService.Object, GetConfiguration("TestCacheService1")),
            };

            CacheServiceFactory factory = new CacheServiceFactory(cacheServices);

            var cache1 = factory.GetCache<TestCacheService1>();

            Assert.IsNotNull(cache1);
            Assert.IsInstanceOfType(cache1, typeof(TestCacheService1));

            Assert.ThrowsExactly<UnregisteredCacheServiceException>(() =>
                factory.GetCache<TestCacheService2>());
        }

        private static ICacheManagerConfiguration GetConfiguration(string name)
        {
            var config = new Dictionary<string, string>
            {
                { "cacheManagers", "" }
            };

            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Configs"))
                .AddJsonFile($"{name}.json");
            return configurationBuilder.Build().GetCacheConfiguration(name);
        }
    }
}
