using System;
using Caesura;
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

            result = Search.getFilesNotContainingTags("video", "mp3");
            Assert.AreEqual(empty, result);

        }

        [Test()]
        public void TestThatGetFilesWithTagsReturnsCorrectResults()
        {
            Search.database = ObjectMother.PopulatedDatabase();

            List<String> result;
            List<String> expected = new List<String>();

            result = Search.getFilesWithTags("video");
            expected.Add(ObjectMother.videoFile);
            expected.Add(ObjectMother.animeFile);
            Assert.AreEqual(expected, result);

            result = Search.getFilesWithTags("anime");
            expected = new List<String>();
            expected.Add(ObjectMother.animeFile);
            Assert.AreEqual(expected, result);

            result = Search.getFilesWithTags("avi");
            expected = new List<String>();
            expected.Add(ObjectMother.videoFile);
            Assert.AreEqual(expected, result);

            result = Search.getFilesWithTags("music");
            expected = new List<String>();
            expected.Add(ObjectMother.musicFile);
            Assert.AreEqual(expected, result);

            result = Search.getFilesWithTags("mp3");
            expected = new List<String>();
            expected.Add(ObjectMother.musicFile);
            Assert.AreEqual(expected, result);

        }


    }
}
