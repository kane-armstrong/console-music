using System.Collections.Generic;
using System.Linq;

namespace Questions.StringOperations
{
    public static class FindMostFrequentWordInSentence
    {
        // at best O(n log(n))
        // Submitted answer
        // Lesson 1 - think in terms of doing things in place rather than jumping straight to sorting
        // due to the expense sorting introduces (computationally)
        // Lesson 2 - worth memorizing computational complexity of typical sort/search algorithms
        public static string FindMostFrequentWord(string sentence)
        {
            var words = sentence.Split(' ');
            if (!words.Any())
            {
                return sentence;
            }

            var dict = new Dictionary<string, int>();

            foreach (var word in words)
            {
                if (!dict.ContainsKey(word))
                {
                    dict.Add(word, 1);
                    continue;
                }
                dict[word]++;
            }

            return dict.ToList().OrderByDescending(x => x.Value).First().Key;
        }

        // should be O(n)
        // This one is obvious without the interview pressure
        // Would probably get bonus points if you asked whether we need to introduce tie breakers
        public static string FindMostFrequentWordSmarter(string sentence)
        {
            var words = sentence.Split(' ');
            if (!words.Any())
            {
                return sentence;
            }

            var dict = new Dictionary<string, int>();
            var mostFrequentWord = words.First();
            var mostFrequentCount = 1;

            foreach (var word in words)
            {
                if (!dict.ContainsKey(word))
                {
                    dict.Add(word, 1);
                    continue;
                }
                dict[word]++;
                if (dict[word] > mostFrequentCount)
                {
                    mostFrequentCount = dict[word];
                    mostFrequentWord = word;
                }
            }

            return mostFrequentWord;
        }
    }
}
