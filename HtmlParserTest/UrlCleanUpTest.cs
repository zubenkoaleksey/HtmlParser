using System;
using HtmlParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlParserTest
{
    [TestClass]
    public class UrlCleanUpTest
    {
        private readonly Uri _startUri = new Uri("http://wikipedia.org");

        [TestMethod]
        public void TestUrlCleanup()
        {
            // Arrange
            string expectedRez = "http://ab.wikipedia.org";
            string currentLink = "//ab.wikipedia.org";

            RunTest(currentLink, expectedRez);
        }

        [TestMethod]
        public void TestUrlCleanupCase2()
        {
            // Arrange
            string expectedRez = "http://ab.wikipedia.org";
            string currentLink = "http://ab.wikipedia.org";

            RunTest(currentLink, expectedRez);
        }

        [TestMethod]
        public void TestUrlCleanupCase3()
        {
            // Arrange
            string expectedRez = "http://wikipedia.org/ab.wikipedia.org";
            string currentLink = "ab.wikipedia.org";

            RunTest(currentLink, expectedRez);
        }

        [TestMethod]
        public void TestUrlCleanupCase4()
        {
            // Arrange
            string expectedRez = "http://wikipedia.org/ab.wikipedia.org";
            string currentLink = "/ab.wikipedia.org";

            RunTest(currentLink, expectedRez);
        }

        [TestMethod]
        public void TestUrlCleanupCase5Error()
        {
            // Arrange
            string expectedRez = "http://wikipedia.org/aasdgasfdgsg";
            string currentLink = "aasdgasfdgsg";

            RunTest(currentLink, expectedRez);
        }


        [TestMethod]
        public void TestUrlCleanupCase6()
        {
            // Arrange
            string expectedRez = "http://aasdgasfdg-dsgfdfg-dgdfg-sg";
            string currentLink = "//aasdgasfdg-dsgfdfg-dgdfg-sg";

            RunTest(currentLink, expectedRez);
        }

        private void RunTest(string currentLink, string expectedRez)
        {
            // Arrange
            var parser = new Parser();

            // Act
            string actual = parser.CleanAndFixUrl(_startUri, currentLink);

            // Assert
            Assert.AreEqual(expectedRez, actual);
        }
    }
}