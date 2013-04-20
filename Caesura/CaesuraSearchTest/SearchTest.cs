using System;
using Server;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;

using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CaesuraSearchTest
{
    public class SearchTest
    {

        [Test()]
        public void TestResultOfNullSearchDatabase()
        {
            Search.database = null;

            List<String> result;
            List<String> empty = new List<String>();

            result = Search.getFilesWithTags("video");
            Assert.AreEqual(empty, result);

            result = Search.getFilesContainingTags("video");
            Assert.AreEqual(empty, result);

            result = Search.getFilesNotContainingTags("video");
            Assert.AreEqual(empty, result);
        }

        [Test()]
        public void TestThatEmptyDatabaseReturnsNoResults()
        {
            Search.database = ObjectMother.EmptyDatabase();

            List<String> result;
            List<String> empty = new List<String>();

            result = Search.getFilesWithTags("video");
            Assert.AreEqual(empty, result);

            result = Search.getFilesContainingTags("video");
            Assert.AreEqual(empty, result);

            result = Search.getFilesNotContainingTags("video");
            Assert.AreEqual(empty, result);
        }

        [Test()]
        public void TestThatNoValidResultsReturnsNoResults()
        {
            Search.database = ObjectMother.PopulatedDatabase();

            List<String> result;
            List<String> empty = new List<String>();

            result = Search.getFilesWithTags("new");
            Assert.AreEqual(empty, result);

            result = Search.getFilesContainingTags("new");
            Assert.AreEqual(empty, result);

            result = Search.getFilesNotContainingTags("video", "mp3", "wav");
            Assert.AreEqual(empty, result);

        }

        [Test()]
        public void TestThatGetFilesWithTagsReturnsCorrectResults()
        {
            Search.database = ObjectMother.PopulatedDatabase();

            List<String> result;
            List<String> expected = new List<String>();

            result = Search.getFilesWithTags("video");
            expected.Add(ObjectMother.animeFile);
            expected.Add(ObjectMother.videoFile);
            Assert.AreEqual(expected, result);

            result = Search.getFilesWithTags("mkv");
            expected = new List<String>();
            expected.Add(ObjectMother.animeFile);
            Assert.AreEqual(expected, result);

            result = Search.getFilesWithTags("avi");
            expected = new List<String>();
            expected.Add(ObjectMother.videoFile);
            Assert.AreEqual(expected, result);

            result = Search.getFilesWithTags("audio");
            expected = new List<String>();
            expected.Add(ObjectMother.musicFile);
            expected.Add(ObjectMother.soundFile);
            Assert.AreEqual(expected, result);

            result = Search.getFilesWithTags("mp3");
            expected = new List<String>();
            expected.Add(ObjectMother.musicFile);
            Assert.AreEqual(expected, result);

            result = Search.getFilesWithTags("audio", "mp3");
            expected = new List<String>();
            expected.Add(ObjectMother.musicFile);
            Assert.AreEqual(expected, result);

            result = Search.getFilesWithTags("audio", "mp3", "wav");
            expected = new List<String>();
            Assert.AreEqual(expected, result);

            result = Search.getFilesWithTags();
            expected = new List<String>();
            Assert.AreEqual(expected, result);

        }

        [Test()]
        public void TestThatGetFilesContainingTagsDoesNotReturnDuplicates()
        {
            Search.database = ObjectMother.PopulatedDatabase();

            List<String> result;
            List<String> expected = new List<String>();

            result = Search.getFilesContainingTags("audio", "mp3");
            expected = new List<String>();
            expected.Add(ObjectMother.musicFile);
            expected.Add(ObjectMother.soundFile);
            Assert.AreEqual(expected, result);

            result = Search.getFilesContainingTags("video", "video");
            expected = new List<String>();
            expected.Add(ObjectMother.animeFile);
            expected.Add(ObjectMother.videoFile);
            Assert.AreEqual(expected, result);

        }

        [Test()]
        public void TestThatGetFilesContainingTagsReturnsMatching()
        {
            Search.database = ObjectMother.PopulatedDatabase();

            List<String> result;
            List<String> expected = new List<String>();

            result = Search.getFilesContainingTags();
            expected = new List<String>();
            Assert.AreEqual(expected, result);

            result = Search.getFilesContainingTags("mkv", "mp3");
            expected = new List<String>();
            expected.Add(ObjectMother.animeFile);
            expected.Add(ObjectMother.musicFile);
            Assert.AreEqual(expected, result);

            result = Search.getFilesContainingTags("anime", "avi", "audio");
            expected = new List<String>();
            expected.Add(ObjectMother.soundFile);
            expected.Add(ObjectMother.animeFile);
            expected.Add(ObjectMother.videoFile);
            expected.Add(ObjectMother.musicFile);
            result.Sort();
            expected.Sort();
            Assert.AreEqual(expected, result);

        }

        [Test()]
        public void TestThatGetFilesNotContainingTagsReturnsCorrectly()
        {
            Search.database = ObjectMother.PopulatedDatabase();

            List<String> result;
            List<String> expected = new List<String>();

            result = Search.getFilesNotContainingTags();
            expected = new List<String>();
            expected.Add(ObjectMother.videoFile);
            expected.Add(ObjectMother.animeFile);
            expected.Add(ObjectMother.musicFile);
            expected.Add(ObjectMother.soundFile);
            result.Sort();
            expected.Sort();
            Assert.AreEqual(expected, result);

            result = Search.getFilesNotContainingTags("mkv", "mp3", "anime", "video");
            expected = new List<String>();
            expected.Add(ObjectMother.soundFile);
            Assert.AreEqual(expected, result);

            result = Search.getFilesNotContainingTags("video");
            expected = new List<String>();
            expected.Add(ObjectMother.soundFile);
            expected.Add(ObjectMother.musicFile);
            result.Sort();
            expected.Sort();
            Assert.AreEqual(expected, result);

            result = Search.getFilesNotContainingTags("mkv", "mp3", "audio", "video");
            expected = new List<String>();
            Assert.AreEqual(expected, result);

        }

    }
}
