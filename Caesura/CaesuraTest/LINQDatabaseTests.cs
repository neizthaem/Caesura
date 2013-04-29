using System;
using Server;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;

using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CaesuraTest
{
    class LINQDatabaseTests
    {
        [Test()]
        public void TestAddFileCaeFile()
        {
            Search.database = ObjectMother.EmptyDatabase();

            CaesFile file = new CaesFile();
            file.Path = "test1File.txt";
            file.Name = "Add File Test";

            Search.database.addFile(file);

            var fileActual = (from f in Search.database.Files
                              select f).First();

            Assert.AreEqual(file.Path, fileActual.Path);
            Assert.AreEqual(file.Name, fileActual.Name);
        }

        [Test()]
        public void TestAddFileCaeFileOther()
        {
            Search.database = ObjectMother.EmptyDatabase();

            CaesFile file = new CaesFile();
            file.Path = "test1File.txt";
            file.Name = "Add File Test";

            Search.database.addFile("test1File.txt", "Add File Test");

            var fileActual = (from f in Search.database.Files
                              select f).First();

            Assert.AreEqual(file.Path, fileActual.Path);
            Assert.AreEqual(file.Name, fileActual.Name);
        }

        [Test()]
        public void TestAddTagName()
        {
            Search.database = ObjectMother.EmptyDatabase();

            TagNames tag = new TagNames();
            tag.TagName = "Test Tag";

            Search.database.addTag(tag);

            var tagActual = (from f in Search.database.TagNames
                              select f).First();

            Assert.AreEqual(tag.TagName, tagActual.TagName);
        }

        [Test()]
        public void TestAddTagNameOther()
        {
            Search.database = ObjectMother.EmptyDatabase();

            TagNames tag = new TagNames();
            tag.TagName = "Test Tag";

            Search.database.addTag("Test Tag");

            var tagActual = (from f in Search.database.TagNames
                             select f).First();

            Assert.AreEqual(tag.TagName, tagActual.TagName);
        }


        // Test Depends on other tests (above) to be working correctly
        [Test()]
        public void TestAddTagEntryForFile()
        {
            Search.database = ObjectMother.EmptyDatabase();

            Search.database.addFile("test.txt", "Test File");
            Search.database.addTag("text");

            Search.database.addTagForFile("test.txt", "text");

            var actual = (from f in Search.database.Tags
                             select f).First();

            Assert.AreEqual("test.txt", actual.FilePath);
            Assert.AreEqual("text", actual.TagName);
        }

        // Test Depends on other tests (above) to be working correctly
        [Test()]
        public void TestAddTagEntryForFileOther()
        {
            Search.database = ObjectMother.EmptyDatabase();

            CaesFile file = new CaesFile();
            file.Path = "test.txt";
            file.Name = "Test File";

            Search.database.addFile(file);
            Search.database.addTag("text");

            Search.database.addTagForFile(file, "text");

            var actual = (from f in Search.database.Tags
                          select f).First();

            Assert.AreEqual("test.txt", actual.FilePath);
            Assert.AreEqual("text", actual.TagName);
        }

    }
}
