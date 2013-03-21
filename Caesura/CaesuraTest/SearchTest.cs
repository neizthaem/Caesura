using System;
using Caesura;
using NUnit.Framework;

namespace CaesuraTest
{
    [TestFixture()]
    public class SearchTest
    {

        [Test()]
        public void TestFileExists()
        {
            Boolean b = Search.FileExists("SearchTestFiles/", "test_file.txt");
            Assert.AreEqual(true, Search.FileExists("SearchTestFiles/", "test_file.txt"));
        }

        [Test()]
        public void TestGetTags()
        {

        }

        [Test()]
        public void TestFilesTaggedAs()
        {

        }

        [Test()]
        public void TestFilesContainingTags()
        {

        }

    }
}
