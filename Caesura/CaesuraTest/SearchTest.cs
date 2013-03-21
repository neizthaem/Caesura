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

    }
}
