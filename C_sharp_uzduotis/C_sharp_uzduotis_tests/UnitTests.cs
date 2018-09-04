using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C_sharp_uzduotis;
namespace C_sharp_uzduotis_tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void Check_If_It_Reads_Correctly_From_File()
        {
            var testLines = new String[] {"žodis žodis žodis"};
            var lines = Program.ReadingTextFromFile("readText.txt");
            Assert.IsNotNull(lines);
            Assert.AreEqual(testLines[0], lines[0]);
        }


        [TestMethod]
        public void Check_if_Line_Splits_Into_Words()
        {
            string testLine = "Lorem ipsum fames hac arcu enim.";
            string[] testWords = new string[] {"Lorem", "ipsum", "fames", "hac", "arcu", "enim"};
            string[] words = Program.SplitingLineIntoWords(testLine);

            CollectionAssert.AreEqual(testWords, words);
        }

        [TestMethod]
        public void Check_If_It_Adds_Word_To_Last_Line_When_List_Is_Not_Empty()
        {
            var resultLines = new List<string>();
            string word = "Lorem";
            resultLines.Add("");
            int leftsize = 6;
            Program.AddWordToLastLine(ref resultLines, word, ref leftsize);
            Assert.AreEqual(word, resultLines.Last());
            Assert.AreEqual(1, leftsize);
        }

        [TestMethod]
        public void Check_When_List_Empty_Add_New_Line()
        {
            var resultLines = new List<string>();
            string word = "Lorem";
            int leftsize = 6;
            Program.AddWordToLastLine(ref resultLines, word, ref leftsize);
            Assert.AreEqual(word, resultLines.Last());
            Assert.AreEqual(1, leftsize);
        }

        [TestMethod]
        public void Check_When_Word_Is_Longer_Than_Size_Then_Split_Into_Lines()
        {
            string word = "Lorem";
            var resultLines = new List<string>();
            var expectedResult = new List<string>() {"Lo", "re", "m"};
            int size = 2;
            string formatedLine = "";
            Program.SplitsWordInLinesIfItIsTooLong(word,ref formatedLine, size, ref resultLines);

            CollectionAssert.AreEqual(expectedResult, resultLines);
            Assert.AreEqual("m", formatedLine);
        }

        [TestMethod]
        public void Check_When_Line_Length_Less_Than_Size_After_Spliting_Word_Add_Space_To_Last_Line()
        {
            string formatedLine = "test";
            int leftsize = 0;
            int size = 5;
            var resultLines = new List<string>() {"test"};
            Program.ChecksIfThereIsAnySpaceLeftInLine(formatedLine,size,ref leftsize, ref resultLines);
            Assert.AreEqual("test", resultLines.Last());
            Assert.AreEqual(1, leftsize);
        }

        [TestMethod]
        public void Check_If_New_Line_Have_Been_Added_With_Word()
        {
            var resultLines = new List<string>() { "test" };
            string word = "Lorem";
            int leftsize = 5;
            Program.AddsANewLine(ref resultLines, word, ref leftsize);
            Assert.AreEqual("Lorem",resultLines[resultLines.Count - 1]);
            Assert.AreEqual(0, leftsize);
        }

        [TestMethod]
        public void Check_If_Results_Are_Correct_With_Given_Words_Array()
        {
            string line = "šiuolaikiškas ir mano žodis";
            int size = 7;
            string[] words = Program.SplitingLineIntoWords(line);
            var resultLines = new List<string>();
            var expectedResults = new List<string>() {"šiuolai", "kiškas", "ir mano", "žodis"};
            resultLines = Program.SplitingWordsInLines(words, resultLines, size);
            CollectionAssert.AreEqual(expectedResults, resultLines);
        }

        [TestMethod]
        public void Check_If_Results_Are_Correct_With_Given_Line_Array()
        {
            var resultLines = new List<string>();
            var testData = new String[] {"žodis žodis žodis", "šiuolaikiškas ir mano žodis"};
            var expectedResults = new List<string>() { "žodis žodis", "žodis", "šiuolaikiškas", "ir mano žodis" };
            var size = 13;
            resultLines = testData.Select(Program.SplitingLineIntoWords)
               .Aggregate(resultLines, (current, words) => Program.SplitingWordsInLines(words, current, size));
            CollectionAssert.AreEqual(expectedResults, resultLines);
        }

       [TestMethod]
        public void Check_If_Writing_To_File_Works()
        {
            var resultLines = new List<string>() { "test" };
            Program.WritingWordsToFile(resultLines, "writeText.txt");
            string[] result = Program.ReadingTextFromFile("writeText.txt");
            Assert.AreEqual("test", result[0]);
        }

       

    }
}
