using System.Collections.Generic;

namespace UnitTests.StringOperations.FindMostFrequentWordInSentenceSpec
{
    public class TestResources
    {
        public static IEnumerable<object[]> TestDataSet => LoadTestDataSet();

        private static IEnumerable<object[]> LoadTestDataSet()
        {
            return new List<object[]>
            {
                new object[] { "hello world hello world hello world world", "world" }
            };
        }
    }
}