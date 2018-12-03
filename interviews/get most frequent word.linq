<Query Kind="Program" />

void Main()
{
	GetMostFrequentWord("hello world hello world hello world world").Dump();
}

public string GetMostFrequentWord(string sentence)
{
	var words = sentence.Split(' ');
	if (!words.Any())
	{
		return sentence;
	}
	
	var dict = new Dictionary<string, int>();
	var mostFrequentWord = words.First();
	var mostFrequentCount = 1;
	
	foreach(var word in words)
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

