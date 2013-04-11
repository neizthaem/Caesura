using System;
using NUnit.Framework;
using Caesura;
using Rhino.Mocks;

using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace CaesuraSearchTest
{
    class SearchTest
    {

        private MockRepository mocks;
        private IDatabase mockDatabase;
        private Tag movie1 = new Tag
        {
            FilePath = "/mnt/video/video.avi",
            TagName = "video"
        };

        private Tag movie2 = new Tag
        {
            FilePath = "/mnt/video/video.avi",
            TagName = "avi"
        };

        private Tag music1 = new Tag
        {
            FilePath = "/mnt/video/music.mp3",
            TagName = "audio"
        };

        private Tag music2 = new Tag
        {
            FilePath = "/mnt/video/music.mp3",
            TagName = "mp3"
        };

        private Tag anime1 = new Tag
        {
            FilePath = "/mnt/video/anime.mkv",
            TagName = "video"
        };

        private Tag anime2 = new Tag
        {
            FilePath = "/mnt/video/anime.mkv",
            TagName = "anime"
        };

        [SetUp()]
        public void Setup()
        {
            mocks = new MockRepository();
            mockDatabase = mocks.Stub<IDatabase>();


        }


    }
}
