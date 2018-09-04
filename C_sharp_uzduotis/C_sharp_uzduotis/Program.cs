using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_sharp_uzduotis
{
    public class Program
    {
        private const string ReadingFile = "readText.txt";
        private const string WritingFile = "writeText.txt";

        public static void Main(string[] args)
        {
            
            var resultLines = new List<string>();
            var size = InputOfLettersLengthInLine();
            var lines = ReadingTextFromFile(ReadingFile);
            resultLines = lines.Select(SplitingLineIntoWords)
                .Aggregate(resultLines, (current, words) => SplitingWordsInLines(words, current, size));
            WritingWordsToFile(resultLines, WritingFile);
        }

        private static int InputOfLettersLengthInLine()
        {
            int size;
            Console.WriteLine("Įveskite raidžių kiekį eilutėje: ");
            var input = Console.ReadLine();
            while (!int.TryParse(input, out size))
            {
                Console.WriteLine("Įvedėte ne skaičių");
                Console.WriteLine("Įveskite raidžių kiekį eilutėje: ");
                input = Console.ReadLine();
            }
            return size;
        }

        public static string[] ReadingTextFromFile(string filename) 
        {
            return System.IO.File.ReadAllLines(@filename, Encoding.UTF8); 
        }

        public static string[] SplitingLineIntoWords(string line)
        {
            return line.Split(new string[] {" ", "."}, StringSplitOptions.RemoveEmptyEntries);
        }

        public static List<string> SplitingWordsInLines(string[] words, List<string> resultLines, int size)
        { 
            string formatedLine = "";

            int leftSpaceInLine = LeftSpaceInLineDependsOnLastLineOfList(resultLines, size);


            foreach (var word in words)
            {
                if (word.Length <= leftSpaceInLine)
                   AddWordToLastLine(ref resultLines, word, ref leftSpaceInLine);
                
                else if (word.Length > leftSpaceInLine)
                {
                    leftSpaceInLine = size;
                    if (word.Length > size)
                    {
                        SplitsWordInLinesIfItIsTooLong(word, ref formatedLine, size, ref resultLines);
                        ChecksIfThereIsAnySpaceLeftInLine(formatedLine, size, ref leftSpaceInLine, ref resultLines);
                    }
                    else
                        AddsANewLine(ref resultLines, word, ref leftSpaceInLine);
                }
            }
            return resultLines;
        }

        public static int LeftSpaceInLineDependsOnLastLineOfList(List<string> resultLines, int size)
        {
            if (resultLines.Count != 0)
            {
                return size - resultLines[resultLines.Count - 1].Length;
            }
            return size;
        }

        public static void AddsANewLine(ref List<string> resultLines, string word, ref int leftsize)
        {
            resultLines.Add(word);
            leftsize -= word.Length;
        }

        public static void ChecksIfThereIsAnySpaceLeftInLine(string formatedLine, int size, ref int leftsize, ref List<string> resultLines)
        {
            if (formatedLine.Length != size)
            {
                leftsize = size - formatedLine.Length;
            }
        }

        public static void SplitsWordInLinesIfItIsTooLong(string word, ref string formatedLine, int size,
            ref List<string> resultLines)
        {
            int startindex = 0;

            while (startindex < word.Length)
            {
                formatedLine = word.Substring(startindex, word.Length < startindex + size
                    ? word.Length - startindex : size);
                startindex += size;
                resultLines.Add(formatedLine);
            }
        }

        public static void AddWordToLastLine(ref List<string> resultLines, string word, ref int leftsize)
        {
            if (resultLines.Count != 0)
                if(resultLines.Last() == "")
                    resultLines[resultLines.Count - 1] += word;
                else
                    resultLines[resultLines.Count - 1] += " " + word;
            else
                resultLines.Add(word);
            leftsize -= word.Length;
        }

        public static void WritingWordsToFile(List<string> results, string filename)
        {
            using (var file = new System.IO.StreamWriter(@filename))
            {
                foreach (var c in results)
                {
                    file.WriteLine(c);
                }
            }
        }


    }
}
