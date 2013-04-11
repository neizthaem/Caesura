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
            Assert.AreSame(empty, result);

            result = Search.getFilesContainingTags("video");
            Assert.AreSame(empty, result);

            result = Search.getFilesNotContainingTags("video");
            Assert.AreSame(empty, result);
        }

        [Test()]
        public void TestThatNoValidResultsReturnsNoResults()
        {
            Search.database = ObjectMother.PopulatedDatabase();

            List<String> result;
            List<String> empty = new List<String>();

            result = Search.getFilesWithTags("new");
            Assert.AreSame(empty, result);

            result = Search.getFilesContainingTags("new");
            Assert.AreSame(empty, result);

            result = Search.getFilesNotContainingTags("video", "mp3");
            Assert.AreSame(empty, result);

        }


    }
}
