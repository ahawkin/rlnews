using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using rlnews.importer;

namespace rlnews.tests
{
    /// <summary>
    /// Unit test class for rlnews.importer
    /// </summary>
    [TestClass]
    public class ImporterTest
    {
        [TestMethod]
        public void TestMatchScore()
        {
            StartImport start = new StartImport();

            string test1 = "Test string";
            string test2 = "Test string";
            int expectedScore = 12;
            int actualScore = start.TestDistance(test1, test2);

            Assert.AreEqual(expectedScore, actualScore, "The strings were correctly matched.");
        }

        [TestMethod]
        public void TestRemoveStopWords()
        {
            StopWords stop = new StopWords();

            string test = "This is a test string";
            string exceptedResult = "test string";
            string actualResult = StopWords.RemoveStopwords(test);

            Console.WriteLine("Expected: " + exceptedResult);
            Console.WriteLine("Result: " + actualResult);

            Assert.AreEqual(exceptedResult, actualResult, "Stop words were successfully removed.");
        }

        [TestMethod]
        public void TestValidateItem()
        {
            ValidateItem validate = new ValidateItem();

            string input = "This is a really long test string to make sure the string is trimmed correctly by the validate method. This is a really long test string to make sure the string is trimmed correctly by the validate method.";
            int exceptedLength = 150;
            int actualLength = validate.TrimNewsLength(input).Length;

            Console.WriteLine("Expected: " + exceptedLength);
            Console.WriteLine("Result: " + actualLength);

            Assert.AreEqual(exceptedLength, actualLength, "String successfully trimmed to 150 characters.");
        }
    }
}
