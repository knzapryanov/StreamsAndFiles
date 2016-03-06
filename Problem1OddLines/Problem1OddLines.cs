using System;
using System.IO;
using System.Linq;

class Problem1OddLines
{
    public static void Main(string[] args)
    {
        StreamReader reader = new StreamReader("LoremIpsum.txt");

        using (reader)
        {
            int lineNumber = 0;
            string line = reader.ReadLine();
            while (line != null)
            {
                if (lineNumber % 2 == 1)
                {
                    Console.WriteLine(line);
                }
                line = reader.ReadLine();
                lineNumber++;
            }
        }

        Console.Write("Press any key to continue . . . ");
        Console.ReadKey(true);
    }
}