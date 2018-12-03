using FluentAssertions;
using Questions.StringOperations;
using Xunit;

namespace UnitTests.StringOperations.FindMostFrequentWordInSentenceSpec
{
    public class Smarter_version
    {
        [Theory]
        [MemberData(nameof(TestResources.TestDataSet), MemberType = typeof(TestResources))]
        public void should_return_correct_word(string sentence, string word)
        {
            FindMostFrequentWordInSentence.FindMostFrequentWordSmarter(sentence).Should().Be(word);
        }
    }
}