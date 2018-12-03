using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using FluentAssertions;
using Questions.StringOperations;
using Xunit;

namespace UnitTests.StringOperations.FindMostFrequentWordInSentenceSpec
{
    public class Sorted_version
    {
        [Theory]
        [MemberData(nameof(TestResources.TestDataSet), MemberType = typeof(TestResources))]
        public void should_return_correct_word(string sentence, string word)
        {
            FindMostFrequentWordInSentence.FindMostFrequentWord(sentence).Should().Be(word);
        }
    }
}
