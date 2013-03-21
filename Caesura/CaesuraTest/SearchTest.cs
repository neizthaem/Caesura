using System;
using System.Text;
using System.IO;
using Caesura;
using NUnit.Framework;
using System.Collections.Generic;

namespace CaesuraTest
{
    [TestFixture()]
    public class SearchTest
    {

        // Test Helper Method to generate string lists easily
        private List<String> list(params String[] arr) {
            List<String> l = new List<String>();
            foreach (String s in arr) { l.Add(s); }
            return l;
        }

        [TestFixtureSetUp]
        public void Init()
        {
            if (!(Directory.Exists(Search.tagDir)))
            {
                Directory.CreateDirectory(Search.tagDir);
            }
        }

        [TestFixtureTearDown]
        public void Cleanup() {
            //Directory.Delete("C:\\Caesura", true);
        }

        [Test()]
        [ExpectedException()]
        public void TestBuildTagSubEmptyPath()
        {
            String path = "";
            String expected = "C:\\Caesura\\tags";
            String actual = Search.buildTagSubDir(path);
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void TestBuildTagSubCRootPath()
        {
            String path = "C:";
            String expected = "C:\\Caesura\\tags";
            String actual = Search.buildTagSubDir(path);
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        [ExpectedException()]
        public void TestBuildTagSubInvalidRecursivePath()
        {
            String path = "C:\\Caesura\\tags";
            String expected = "C:\\Caesura\\tags\\Users\\Austin\\.ssh\\Caesura\\Caesura\\CaesuraTest\\bin\\Debug\\SearchTestFiles";
            String actual = Search.buildTagSubDir(path);
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void TestBuildTagSubDirValidPath()
        {
            String path = "C:\\Users\\Austin\\.ssh\\Caesura\\Caesura\\CaesuraTest\\bin\\Debug\\SearchTestFiles";
            String expected = "C:\\Caesura\\tags\\Users\\Austin\\.ssh\\Caesura\\Caesura\\CaesuraTest\\bin\\Debug\\SearchTestFiles";
            String actual = Search.buildTagSubDir(path);
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        [ExpectedException()]
        public void TestRestoreTagSubEmptyPath()
        {
            String path = "C:\\Caesura\\tags";
            String expected = "";
            String actual = Search.restoreTagSubDir(path);
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void TestRestoreTagSubCRootPath()
        {
            String path = "C:\\Caesura\\tags";
            String expected = "C:";
            String actual = Search.restoreTagSubDir(path);
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        [ExpectedException()]
        public void TestRestoreTagSubInvalidRecursivePath()
        {
            String path = "C:\\Caesura\\tags\\Users\\Austin\\.ssh\\Caesura\\Caesura\\CaesuraTest\\bin\\Debug\\SearchTestFiles";
            String expected = "C:\\Caesura\\tags";
            String actual = Search.restoreTagSubDir(path);
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void TestRestoreTagSubDirValidPath()
        {
            String path = "C:\\Caesura\\tags\\Users\\Austin\\.ssh\\Caesura\\Caesura\\CaesuraTest\\bin\\Debug\\SearchTestFiles";
            String expected = "C:\\Users\\Austin\\.ssh\\Caesura\\Caesura\\CaesuraTest\\bin\\Debug\\SearchTestFiles";
            String actual = Search.restoreTagSubDir(path);
            Assert.AreEqual(expected, actual);
        }

        [Test()]
        public void TestAddSearchTagAddsDirectory()
        {
            String path = Directory.GetCurrentDirectory() + "\\SearchTestFiles";
            String tagPath = Search.buildTagSubDir(path);
            Directory.Delete(tagPath, true); // ensures that it didn't just already exist
            Search.addSearchTagEntry(path, "animeFile.txt", "anime");
            Boolean created = false;
            if (Directory.Exists(tagPath))
            {
                created = true;
            }
            Assert.AreEqual(true, created);
        }

        [Test()]
        public void TestAddSearchTagAddsInfoFile()
        {
            String path = Directory.GetCurrentDirectory() + "\\SearchTestFiles";
            String tagPath = Search.buildTagSubDir(path);
            Directory.Delete(tagPath, true); // ensures that it didn't just already exist
            Search.addSearchTagEntry(path, "animeFile.txt", "anime");
            Boolean created = false;
            if (File.Exists(tagPath + "\\taginfo"))
            {
                created = true;
            }
            Assert.AreEqual(true, created);
        }

        [Test()]
        public void TestAddSearchTagAppendsToInfoFile()
        {
            String path = Directory.GetCurrentDirectory() + "\\SearchTestFiles";
            String tagPath = Search.buildTagSubDir(path);
            Directory.Delete(tagPath, true); // ensures that it didn't just already exist
            Search.addSearchTagEntry(path, "animeFile.txt", "anime", "video");
            Search.addSearchTagEntry(path, "musicFile.txt", "audio");
            String[] lines = File.ReadAllLines(tagPath + "\\taginfo");
            Assert.AreEqual("animeFile.txt" + '\t' + "anime" + '\t' + "video", lines[0]);
            Assert.AreEqual("musicFile.txt" + '\t' + "audio", lines[1]);
        }

        [Test()]
        public void TestRemoveSearchTagEntryValid()
        {
            String path = Directory.GetCurrentDirectory() + "\\SearchTestFiles";
            String tagPath = Search.buildTagSubDir(path);
            Directory.Delete(tagPath, true); // ensures that it didn't just already exist
            Search.addSearchTagEntry(path, "animeFile.txt", "anime", "video");
            Search.addSearchTagEntry(path, "musicFile.txt", "audio");
            Search.addSearchTagEntry(path, "pictureFile.txt", "picture");
            Search.removeSearchTagEntry(path, "animeFile.txt");
            String[] lines = File.ReadAllLines(tagPath + "\\taginfo");
            Assert.AreEqual("musicFile.txt" + '\t' + "audio", lines[0]);
            Assert.AreEqual("pictureFile.txt" + '\t' + "picture", lines[1]);
        }

    }
}
