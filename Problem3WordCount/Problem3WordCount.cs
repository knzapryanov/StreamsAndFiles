using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Problem3WordCount
{
    public static void Main()
    {
        StreamReader readerText = new StreamReader("text.txt");
        StreamReader readerWords = new StreamReader("words.txt");
        StreamWriter writer = new StreamWriter("result.txt");

        using (readerText)
        {
            using (readerWords)
            {
                using (writer)
                {
                    string allText = readerText.ReadToEnd();
                    string word = readerWords.ReadLine();
                    Dictionary<string, int> wordCount = new Dictionary<string, int>();
                    while (word != null)
                    {
                        MatchCollection currentWordMatchColection = Regex.Matches(allText, @"\b(?i)" + word + @"\b");
                        wordCount[word] = currentWordMatchColection.Count;
                        word = readerWords.ReadLine();
                    }
                    var sortedWordCount = from pair in wordCount orderby pair.Value descending select pair;
                    foreach (var pair in sortedWordCount)
                    {
                        writer.WriteLine("{0} - {1}", pair.Key, pair.Value);
                    }
                }
            }
        }

        Console.Write("Press any key to continue . . . ");
        Console.ReadKey(true);
    }
}