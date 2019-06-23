using AspectCache.Core.Components;
using AspectCache.Tests.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspectCache.Tests
{
    [TestClass]
    public class CacheServiceFactoryTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "cacheServices")]
        public void Constructor_ShouldThrowExceptionForNullServiceList()
        {
            new CacheServiceFactory(null);
        }

        [TestMethod]
        public void Constructor_HappyPath()
        {
            Mock<BaseCacheService> mockCacheService = new Mock<BaseCacheService>();

            CacheServiceFactory factory = new CacheServiceFactory(new[] { mockCacheService.Object }.AsEnumerable());

            Assert.IsNotNull(factory);
        }

        [TestMethod]
        public void GetCache_ShouldReturnCacheOfAppropriateType()
        {
            List<BaseCacheService> cacheServices = new List<BaseCacheService>
            {
                new TestCacheService1(),
                new TestCacheService2()
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
        [ExpectedException(typeof(UnregisteredCacheServiceException), "Unregistered CacheService requested: TestCacheService2")]
        public void GetCache_ShouldThrowExceptionWhenUnregisteredTypeRequested()
        {
            List<BaseCacheService> cacheServices = new List<BaseCacheService>
            {
                new TestCacheService1()
            };

            CacheServiceFactory factory = new CacheServiceFactory(cacheServices);

            var cache1 = factory.GetCache<TestCacheService1>();

            Assert.IsNotNull(cache1);
            Assert.IsInstanceOfType(cache1, typeof(TestCacheService1));

            factory.GetCache<TestCacheService2>();
        }
    }
}
