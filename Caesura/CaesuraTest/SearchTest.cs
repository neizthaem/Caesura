using System;
using Caesura;
using NUnit.Framework;
using System.Collections.Generic;

namespace CaesuraTest
{
    [TestFixture()]
    public class SearchTest
    {

        private List<String> list(params String[] arr)
        {
            List<String> l = new List<String>();
            foreach (String s in arr)
            {
                l.Add(s);
            }
            return l;
        }

        [Test()]
        public void TestFileExists()
        {
            Boolean b = Search.FileExists("SearchTestFiles/", "info.tags");
            Assert.AreEqual(true, b);
        }

        [Test()]
        public void TestGetTags()
        {
            List<String> expectedTags = list("anime", "video");
            List<String> actualTags = Search.GetTags("SearchTestFiles/", "animeFile.txt");
            Assert.AreEqual(expectedTags, actualTags);
        }

        [Test()]
        public void TestGetTagsFileNotTagged()
        {
            List<String> expectedTags = list("untagged");
            List<String> actualTags = Search.GetTags("SearchTestFiles/", "untagged.txt");
            Assert.AreEqual(expectedTags, actualTags);
        }

        [Test()]
        public void TestGetTagsFileNotFound()
        {
            List<String> expectedTags = list();
            List<String> actualTags = Search.GetTags("SearchTestFiles/", "unknown.txt");
            Assert.AreEqual(expectedTags, actualTags);
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
