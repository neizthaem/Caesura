using System;
using System.IO;
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

        /*[Test()]
        public void TestGetTagsNoTaggingFile()
        {
            List<String> expectedTags = list();
            List<String> actualTags = Search.GetTags("", "unknown.txt");
            Assert.AreEqual(expectedTags, actualTags);
            if (!File.Exists("info.tags"))
            {
                Assert.AreEqual("info.tags created", "info.tags NOT created");
            }
        }*/

        [Test()]
        public void TestFilesTaggedAs()
        {
            List<String> expected = list("animeFile.txt");
            List<String> actual = Search.GetFilesTaggedAs("anime");
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void TestFilesContainingTags()
        {

        }

    }
}
