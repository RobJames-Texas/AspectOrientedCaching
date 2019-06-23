using System.Collections.Generic;

namespace AspectCache.Tests.Components
{
    public interface ITestingRepository
    {
        void AddOrUpdate(string number, int integer);
        ICollection<string> AvailableNumbers();
        void Delete(string number);
        int StringToInt(string numbers);
    }
}