using AspectCache.Core.Aspect;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspectCache.Tests.Components
{
    public class TestingRepository : ITestingRepository
    {
        private readonly IDictionary<string, int> _keyValuePairs;

        /// <summary>
        /// Simple "Repository" to test aspect caching.
        /// </summary>
        public TestingRepository()
        {
            _keyValuePairs = new Dictionary<string, int>
            {
                { "onetwothree", 123 },
                { "fourfivesix", 456 },
                { "seveneightnine", 789 },
                { "teneleventwelve", 101112 }
            };
        }

        [Cache]
        public int StringToInt(string numbers)
        {
            return _keyValuePairs[numbers];
        }

        [Cache]
        public ICollection<string> AvailableNumbers()
        {
            return _keyValuePairs.Keys;
        }

        [Cache(IgnoreThisParameter = true)]
        public IEnumerable<string> Numbers(int skip, [NotCacheKey]Guid someToken)
        {
            return _keyValuePairs.Keys.Skip(skip);
        }

        [InvalidateCache([nameof(StringToInt), nameof(AvailableNumbers), nameof(Numbers)])]
        public void AddOrUpdate(string number, int integer)
        {
            _keyValuePairs[number] = integer;
        }

        [InvalidateCache([nameof(StringToInt), nameof(AvailableNumbers), nameof(Numbers)])]
        public void Delete(string number)
        {
            _keyValuePairs.Remove(number);
        }
    }
}
